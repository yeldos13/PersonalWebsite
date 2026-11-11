using System.IO.Compression;
using System.Net;
using MaxMind.Db;

namespace Portfolio.Services;

// Country and city by IP from the free DB-IP Lite database (CC BY 4.0), refreshed monthly in the background.
public class GeoIp(IHostEnvironment env, IHttpClientFactory httpFactory, ILogger<GeoIp> logger) : BackgroundService
{
    private readonly string _dir = VisitLog.DataDir(env);
    private volatile Reader? _reader;
    private string? _loaded;

    public (string? Country, string? City) Lookup(IPAddress? ip)
    {
        var reader = _reader;
        if (reader is null || ip is null) return (null, null);
        try
        {
            var data = reader.Find<Dictionary<string, object>>(ip);
            return (Name(data, "country"), Name(data, "city"));
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "GeoIP lookup failed");
            return (null, null);
        }
    }

    private static string? Name(Dictionary<string, object>? data, string key) =>
        data?.GetValueOrDefault(key) is Dictionary<string, object> part
        && part.GetValueOrDefault("names") is Dictionary<string, object> names
            ? names.GetValueOrDefault("en") as string
            : null;

    private string FileFor(DateTime month) => Path.Combine(_dir, $"dbip-city-lite-{month:yyyy-MM}.mmdb");

    protected override async Task ExecuteAsync(CancellationToken stop)
    {
        while (!stop.IsCancellationRequested)
        {
            try
            {
                var now = DateTime.UtcNow;
                // The new month's file appears a few days into the month, so fall back to the previous one.
                var current = new[] { now, now.AddMonths(-1) }.Select(FileFor).FirstOrDefault(File.Exists);
                if (current != FileFor(now))
                    current = await DownloadAsync(now, stop) ?? await DownloadAsync(now.AddMonths(-1), stop) ?? current;
                if (current is not null && current != _loaded)
                {
                    var previous = _loaded;
                    _reader = new Reader(current);
                    _loaded = current;
                    logger.LogInformation("GeoIP database {File} loaded", Path.GetFileName(current));
                    // The previous file may still be mapped by in-flight lookups; it goes on the next month.
                    foreach (var old in Directory.GetFiles(_dir, "dbip-city-lite-*.mmdb*").Where(f => f != current && f != previous))
                        try { File.Delete(old); } catch (IOException) { }
                }
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                logger.LogWarning(e, "GeoIP database update failed");
            }
            await Task.Delay(TimeSpan.FromDays(1), stop);
        }
    }

    private async Task<string?> DownloadAsync(DateTime month, CancellationToken stop)
    {
        var file = FileFor(month);
        if (File.Exists(file)) return file;

        var http = httpFactory.CreateClient();
        http.Timeout = TimeSpan.FromMinutes(10);
        using var response = await http.GetAsync(
            $"https://download.db-ip.com/free/dbip-city-lite-{month:yyyy-MM}.mmdb.gz",
            HttpCompletionOption.ResponseHeadersRead, stop);
        if (!response.IsSuccessStatusCode) return null;

        var tmp = file + ".tmp";
        await using (var gz = new GZipStream(await response.Content.ReadAsStreamAsync(stop), CompressionMode.Decompress))
        await using (var output = File.Create(tmp))
            await gz.CopyToAsync(output, stop);
        File.Move(tmp, file, overwrite: true);
        return file;
    }
}
