using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;

namespace Portfolio.Pages;

public class IndexModel(Profile profile) : PageModel
{
    public Profile Profile { get; } = profile;

    public void OnGet() { }
}
