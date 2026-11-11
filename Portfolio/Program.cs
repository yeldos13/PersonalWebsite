using System.Text.Encodings.Web;
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

var app = builder.Build();

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

var visits = app.Services.GetRequiredService<VisitLog>();
app.Use(async (ctx, next) =>
{
    await next();
    if (!HttpMethods.IsGet(ctx.Request.Method) || ctx.Response.StatusCode != 200) return;
    var path = ctx.Request.Path.Value ?? "";
    if (path.StartsWith("/files/") && path.EndsWith(".pdf"))
        visits.Add(ctx, "cv", ProfileData.Normalize(path[^6..^4]));
});

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

string[] clickTargets = ["telegram", "phone", "email", "github", "linkedin", "project"];
// Page views come from a script beacon, not the page request: scanners that fake a browser user-agent rarely run JS.
app.MapPost("/t", (string e, string? lang, string? r, HttpContext ctx) =>
{
    if (e == "view") visits.Add(ctx, "view", ProfileData.Normalize(lang), r);
    else if (clickTargets.Contains(e)) visits.Add(ctx, "click", e);
    return Results.NoContent();
});

app.Run();
