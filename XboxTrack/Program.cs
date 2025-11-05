using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorPro.BlazorSize;
using XboxTrack;
using XboxTrack.Services;
using XboxTrack.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddMudServices()
    .AddMediaQueryService()
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddSingleton<IClipboardService, ClipboardService>()
    .AddScoped<IXboxPurchaseService, XboxPurchaseService>()
    .AddScoped<PageIndexViewModel>();

await builder.Build().RunAsync();