using Portfolio.Models;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<Profile>(_ => ProfileData.Get());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.MapGet("/api/profile", (Profile profile) => Results.Json(profile));

app.Run();
