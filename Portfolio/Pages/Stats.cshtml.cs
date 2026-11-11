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

    public int Days { get; private set; }
    public string Key { get; private set; } = "";
    public List<Visit> Visits { get; private set; } = [];

    public IActionResult OnGet(string? key, int days = 30)
    {
        if (key is null || !CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(log.Key)))
            return NotFound();
        Key = key;
        Response.Cookies.Append(VisitLog.IgnoreCookie, "1", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(10), HttpOnly = true, Secure = Request.IsHttps, SameSite = SameSiteMode.Lax
        });
        Days = Math.Clamp(days, 1, 365);
        Visits = log.ReadSince(DateTimeOffset.UtcNow.AddDays(-Days));
        return Page();
    }
}
