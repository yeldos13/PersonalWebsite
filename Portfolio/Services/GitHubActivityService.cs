using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;

namespace Portfolio.Services;

public record ContributionDay(DateOnly Date, int Level, int Count);

public record RecentCommit(string Repo, string Message, string Url, DateTimeOffset Date);

public record GitHubActivity(IReadOnlyList<ContributionDay> Days, int TotalLastYear, IReadOnlyList<RecentCommit> Commits);

public partial class GitHubActivityService(HttpClient http, IMemoryCache cache, ILogger<GitHubActivityService> logger)
{
    public const string User = "yeldossozakbay";
    private const string CacheKey = "github-activity";

    public async Task<GitHubActivity?> GetAsync()
    {
        if (cache.TryGetValue(CacheKey, out GitHubActivity? cached)) return cached;

        GitHubActivity? result = null;
        try
        {
            var calendar = LoadCalendarAsync();
            var commits = LoadCommitsAsync();
            var (days, total) = await calendar;
            result = new GitHubActivity(days, total, await commits);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Не удалось получить активность GitHub");
        }

        cache.Set(CacheKey, result, result is null ? TimeSpan.FromMinutes(5) : TimeSpan.FromHours(1));
        return result;
    }

    private async Task<(IReadOnlyList<ContributionDay>, int)> LoadCalendarAsync()
    {
        var html = await http.GetStringAsync($"https://github.com/users/{User}/contributions");

        var counts = TooltipRegex().Matches(html).ToDictionary(
            m => m.Groups["id"].Value,
            m => m.Groups["n"].Value == "No" ? 0 : int.Parse(m.Groups["n"].Value.Replace(",", "")));

        var days = DayRegex().Matches(html)
            .Select(m => new ContributionDay(
                DateOnly.Parse(m.Groups["date"].Value),
                int.Parse(m.Groups["level"].Value),
                counts.GetValueOrDefault(m.Groups["id"].Value)))
            .OrderBy(d => d.Date)
            .ToList();

        var totalMatch = TotalRegex().Match(html);
        var total = totalMatch.Success ? int.Parse(totalMatch.Groups[1].Value.Replace(",", "")) : days.Sum(d => d.Count);
        return (days, total);
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
            using var doc = JsonDocument.Parse(
                await http.GetStringAsync($"https://api.github.com/repos/{User}/{repo}/commits?per_page=5"));
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
        client.Timeout = TimeSpan.FromSeconds(8);
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("sozakbay-asia", "1.0"));
    }

    [GeneratedRegex(@"data-date=""(?<date>\d{4}-\d{2}-\d{2})""\s+id=""(?<id>contribution-day-component-\d+-\d+)""\s+data-level=""(?<level>\d)""")]
    private static partial Regex DayRegex();

    [GeneratedRegex(@"for=""(?<id>contribution-day-component-\d+-\d+)""[^>]*>(?<n>No|[\d,]+) contributions?")]
    private static partial Regex TooltipRegex();

    [GeneratedRegex(@"([\d,]+)\s+contributions?\s+in the last year")]
    private static partial Regex TotalRegex();
}
