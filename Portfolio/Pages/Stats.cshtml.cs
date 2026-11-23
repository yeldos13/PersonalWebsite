using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Services;

namespace Portfolio.Pages;

public class StatsModel(VisitLog log) : PageModel
{
    // Astana is UTC+5 all year.
    public static readonly TimeSpan Astana = TimeSpan.FromHours(5);

    private const string KeyCookie = "stats";

    public int Days { get; private set; }
    public List<Visit> Visits { get; private set; } = [];

    public IActionResult OnGet(string? key, int days = 30)
    {
        Days = Math.Clamp(days, 1, 365);
        if (key is not null)
        {
            if (!Valid(key)) return NotFound();
            // The key moves to a cookie and out of the address bar, history and proxy logs.
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(10), HttpOnly = true, Secure = Request.IsHttps, SameSite = SameSiteMode.Lax
            };
            Response.Cookies.Append(KeyCookie, key, new CookieOptions(options) { SameSite = SameSiteMode.Strict, Path = "/stats" });
            Response.Cookies.Append(VisitLog.IgnoreCookie, "1", options);
            return Redirect($"/stats?days={Days}");
        }
        if (!Valid(Request.Cookies[KeyCookie])) return NotFound();

        Visits = log.ReadSince(DateTimeOffset.UtcNow.AddDays(-Days));
        return Page();
    }

    private bool Valid(string? key) =>
        key is not null && CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(log.Key));
}
