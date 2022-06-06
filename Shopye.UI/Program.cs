using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shopye.UI.Data;
using Shopye.UI.Services;
using Shopye.UI.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7173/") });

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IManageProductsLocalStorageService, ManageProductsLocalStorageService>();
builder.Services.AddScoped<IManageCartItemsLocalStorageService, ManageCartItemsLocalStorageService>();

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

//Blazor Server use SignalR , so this method map connection between server and the client 
/*
 We call MapBlazorHub to map the Blazor Hub to the app’s default path. 
 The Blazor Server script (blazor.server.js) automatically points to the endpoint created by MapBlazorHub.
 blazor.server.jsallows the app to establish a SignalR connection over the network to handle UI updates and event forwarding between the Blazor app running in the browser and our ASP.NET Core app running on the server. 
 You can find a reference to this js script in the Pages\ _Host.cshtml file.
 */
app.MapBlazorHub();
// map every request to this page
app.MapFallbackToPage("/_Host");

app.Run();
