using System.Reflection;
using System.Text.Json;
using MudBlazor.Services;
using PlexFront.Services;
using PlexFront.Utilities;

var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var file = File.ReadAllText(Path.Join(directory, "_Configuration.json"));
var configuration = JsonSerializer.Deserialize<Configuration>(file);
if (configuration is null) return;


var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(8120);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<QBitTorrentService>();
builder.Services.AddMudServices();

builder.Services.AddLogging(options =>
{
    options.ClearProviders();
    options.AddConsole();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
