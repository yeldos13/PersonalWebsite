using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Lang { get; set; }

    public Profile Profile { get; private set; } = null!;
    public UiText Ui { get; private set; } = null!;

    public IActionResult OnGet()
    {
        if (string.Equals(Lang, "ru", StringComparison.OrdinalIgnoreCase))
            return RedirectPermanent("/");

        var lang = ProfileData.Normalize(Lang);
        Profile = ProfileData.Get(lang);
        Ui = UiText.For(lang);
        return Page();
    }
}
