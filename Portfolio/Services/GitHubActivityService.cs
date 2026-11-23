using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;

namespace Portfolio.Services;

public record ContributionDay(DateOnly Date, int Level, int Count);

public record RecentCommit(string Repo, string Message, string Url, DateTimeOffset Date);

public record GitHubActivity(IReadOnlyList<ContributionDay> Days, IReadOnlyList<RecentCommit> Commits);

public partial class GitHubActivityService(HttpClient http, IMemoryCache cache, ILogger<GitHubActivityService> logger)
{
    public const string User = "yeldos13";
    private const string CacheKey = "github-activity";
    private const string LastGoodKey = "github-activity-last";

    // Static: the typed client is transient, but one refresh must be shared by all requests.
    private static readonly Lock RefreshLock = new();
    private static Task<GitHubActivity?>? _refresh;

    // Stale data is served at once while a single background refresh runs; only a cold start waits for GitHub.
    public Task<GitHubActivity?> GetAsync()
    {
        if (cache.TryGetValue(CacheKey, out GitHubActivity? fresh)) return Task.FromResult(fresh);
        Task<GitHubActivity?> refresh;
        lock (RefreshLock) refresh = _refresh ??= RefreshAsync();
        return cache.TryGetValue(LastGoodKey, out GitHubActivity? last) ? Task.FromResult(last) : refresh;
    }

    private async Task<GitHubActivity?> RefreshAsync()
    {
        await Task.Yield(); // _refresh must be set before this task can finish and clear it
        GitHubActivity? result = null;
        try
        {
            var commits = LoadCommitsAsync();
            var days = ParseCalendar(await http.GetStringAsync($"https://github.com/users/{User}/contributions"));
            IReadOnlyList<RecentCommit> recent = [];
            try { recent = await commits; }
            catch (Exception ex) { logger.LogWarning(ex, "Не удалось получить коммиты GitHub"); }
            if (days.Count > 0) result = new GitHubActivity(days, recent);
            else logger.LogWarning("В календаре GitHub не найдено ни одного дня");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Не удалось получить активность GitHub");
        }

        // A failed refresh keeps showing the last good data instead of hiding the section.
        if (result is null) cache.TryGetValue(LastGoodKey, out result);
        else cache.Set(LastGoodKey, result);
        cache.Set(CacheKey, result, result is null ? TimeSpan.FromMinutes(5) : TimeSpan.FromHours(1));
        lock (RefreshLock) _refresh = null;
        return result;
    }

    internal static IReadOnlyList<ContributionDay> ParseCalendar(string html)
    {
        var counts = TooltipRegex().Matches(html).ToDictionary(
            m => m.Groups["id"].Value,
            m => m.Groups["n"].Value == "No" ? 0 : int.Parse(m.Groups["n"].Value.Replace(",", "")));

        return DayRegex().Matches(html)
            .Select(m => new ContributionDay(
                DateOnly.Parse(m.Groups["date"].Value),
                int.Parse(m.Groups["level"].Value),
                counts.GetValueOrDefault(m.Groups["id"].Value)))
            .OrderBy(d => d.Date)
            .ToList();
    }

    private async Task<IReadOnlyList<RecentCommit>> LoadCommitsAsync()
    {
        using var reposDoc = JsonDocument.Parse(
            await http.GetStringAsync($"https://api.github.com/users/{User}/repos?sort=pushed&per_page=3"));
        var repos = reposDoc.RootElement.EnumerateArray()
            .Where(r => !r.GetProperty("fork").GetBoolean())
            .Select(r => r.GetProperty("name").GetString()!)
            .ToList();

        var perRepo = await Task.WhenAll(repos.Select(async repo =>
        {
            HttpResponseMessage response;
            // один медленный репозиторий не должен скрывать коммиты остальных
            try { response = await http.GetAsync($"https://api.github.com/repos/{User}/{repo}/commits?per_page=5"); }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException) { return new List<RecentCommit>(); }
            using var _ = response;
            if (!response.IsSuccessStatusCode) return new List<RecentCommit>();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return doc.RootElement.EnumerateArray().Select(c =>
            {
                var commit = c.GetProperty("commit");
                return new RecentCommit(
                    repo,
                    commit.GetProperty("message").GetString()!.Split('\n')[0],
                    c.GetProperty("html_url").GetString()!,
                    commit.GetProperty("author").GetProperty("date").GetDateTimeOffset());
            }).ToList();
        }));

        return perRepo.SelectMany(x => x).OrderByDescending(c => c.Date).Take(5).ToList();
    }

    public static void Configure(HttpClient client)
    {
        // The contributions page is ~230 KB and GitHub sometimes sends it slowly.
        client.Timeout = TimeSpan.FromSeconds(15);
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("sozakbay-dev", "1.0"));
    }

    [GeneratedRegex(@"data-date=""(?<date>\d{4}-\d{2}-\d{2})""\s+id=""(?<id>contribution-day-component-\d+-\d+)""\s+data-level=""(?<level>\d)""")]
    private static partial Regex DayRegex();

    [GeneratedRegex(@"for=""(?<id>contribution-day-component-\d+-\d+)""[^>]*>(?<n>No|[\d,]+) contributions?")]
    private static partial Regex TooltipRegex();
}
