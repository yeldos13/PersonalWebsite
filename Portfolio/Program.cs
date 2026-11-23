using System.Text.Encodings.Web;
using System.Threading.RateLimiting;
using System.Text.Unicode;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting.WindowsServices;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
});

builder.Services.AddWindowsService(options => options.ServiceName = "Portfolio");

builder.Services.Configure<ForwardedHeadersOptions>(options =>
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);

builder.Services.AddWebEncoders(options =>
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All));

builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<GeoIp>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<GeoIp>());
builder.Services.AddSingleton<VisitLog>();
builder.Services.AddHttpClient<GitHubActivityService>(GitHubActivityService.Configure);
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    // Keep the bare 429: re-executing the 404 page for a POST fails antiforgery with a 400.
    options.OnRejected = (ctx, _) =>
    {
        if (ctx.HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IStatusCodePagesFeature>() is { } pages) pages.Enabled = false;
        return ValueTask.CompletedTask;
    };
    options.AddPolicy("beacon", ctx => RateLimitPartition.GetFixedWindowLimiter(
        ctx.Connection.RemoteIpAddress?.ToString() ?? "",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 30, Window = TimeSpan.FromMinutes(1) }));
});

var app = builder.Build();

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

var visits = app.Services.GetRequiredService<VisitLog>();

app.UseStatusCodePagesWithReExecute("/404");
app.UseStaticFiles();
app.UseRouting();
app.UseRateLimiter();

app.MapRazorPages();

string[] clickTargets = ["telegram", "phone", "email", "github", "linkedin", "project"];
// Views and CV downloads come from a script beacon, not the request itself: scanners that fake a browser user-agent rarely run JS.
app.MapPost("/t", (string e, string? lang, string? r, HttpContext ctx) =>
{
    if (e is "view" or "cv") visits.Add(ctx, e, ProfileData.Normalize(lang), r);
    else if (clickTargets.Contains(e)) visits.Add(ctx, "click", e);
    return Results.NoContent();
}).RequireRateLimiting("beacon");

// Old CV link from before the per-language files.
app.MapGet("/files/Yeldos-Sozakbay-CV.pdf", () => Results.Redirect("/files/Yeldos-Sozakbay-CV-ru.pdf", permanent: true));

app.Run();
