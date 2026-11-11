using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Portfolio.Services;

public record Visit(DateTimeOffset Time, string Kind, string Target, string? Referrer, bool Mobile, string Visitor,
    string? Country = null, string? City = null);

// shortcut: append-only JSON lines file read whole on /stats, fine for a portfolio; move to SQLite past ~1M lines
public partial class VisitLog
{
    private readonly string _file;
    private readonly Lock _lock = new();
    private readonly GeoIp _geo;

    // Outside the publish folder so deploys keep the data; dev runs on the same machine get their own folder.
    public static string DataDir(IHostEnvironment env)
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            env.IsDevelopment() ? "Portfolio-dev" : "Portfolio");
        Directory.CreateDirectory(dir);
        return dir;
    }

    public VisitLog(IHostEnvironment env, GeoIp geo)
    {
        _geo = geo;
        var dir = DataDir(env);
        _file = Path.Combine(dir, "visits.jsonl");
        var keyFile = Path.Combine(dir, "stats-key.txt");
        if (!File.Exists(keyFile))
            File.WriteAllText(keyFile, Convert.ToHexString(RandomNumberGenerator.GetBytes(16)).ToLowerInvariant());
        Key = File.ReadAllText(keyFile).Trim();
    }

    // Secret for /stats?key=..., also salts visitor hashes so they can't be reversed to an IP.
    public string Key { get; }

    public void Add(HttpContext ctx, string kind, string target)
    {
        var ua = ctx.Request.Headers.UserAgent.ToString();
        if (ua.Length == 0 || BotRegex().IsMatch(ua)) return;

        var now = DateTimeOffset.UtcNow;
        var ip = ctx.Connection.RemoteIpAddress;
        // Day in the hash: a visitor is unique per day, never tracked across days.
        var visitor = Convert.ToHexString(SHA256.HashData(
            Encoding.UTF8.GetBytes($"{Key}|{now:yyyy-MM-dd}|{ip}|{ua}")))[..12];

        var (country, city) = _geo.Lookup(ip);

        string? referrer = null;
        if (Uri.TryCreate(ctx.Request.Headers.Referer.ToString(), UriKind.Absolute, out var r)
            && !r.Host.EndsWith("sozakbay.dev") && !r.Host.EndsWith("sozakbay.asia") && r.Host != "localhost")
            referrer = r.Host.StartsWith("www.") ? r.Host[4..] : r.Host;

        var line = JsonSerializer.Serialize(new Visit(now, kind, target, referrer, ua.Contains("Mobi"), visitor, country, city));
        lock (_lock) File.AppendAllText(_file, line + "\n");
    }

    public List<Visit> ReadSince(DateTimeOffset from)
    {
        if (!File.Exists(_file)) return [];
        string[] lines;
        lock (_lock) lines = File.ReadAllLines(_file);
        return lines.Select(l => JsonSerializer.Deserialize<Visit>(l)!).Where(v => v.Time >= from).ToList();
    }

    [GeneratedRegex(@"bot|crawl|spider|slurp|curl|wget|python|java/|go-http|headless|preview|monitor|scan|facebookexternalhit|okhttp|axios|httpclient",
        RegexOptions.IgnoreCase)]
    private static partial Regex BotRegex();
}
