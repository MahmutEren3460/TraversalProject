using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Traversal.CQRS.Handlers.DestinationHandlers;
using Traversal.CQRS.Handlers.GuideHandlers;
using Traversal.Models;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Serilog yapýlandýrmasý
builder.Services.AddLogging(log =>
{
    log.ClearProviders();
    log.SetMinimumLevel(LogLevel.Debug);
    log.AddFile($"{Directory.GetCurrentDirectory()}\\LogFile\\log.txt", LogLevel.Error);
    log.AddDebug();
});

// CQRS Handlers
builder.Services.AddScoped<GetAllDestinationQueryHandler>();
builder.Services.AddScoped<GetDestinationByIDQueryHandler>();
builder.Services.AddScoped<CreateDestinationCommandHandler>();
builder.Services.AddScoped<RemoveDestinationCommandHandler>();
builder.Services.AddScoped<UpdateDestinationCommandHandler>();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// DbContext
builder.Services.AddDbContext<Context>();

// Identity
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddErrorDescriber<CustomIdentityValidator>()
    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

// HttpClient
builder.Services.AddHttpClient();

// Dependency Injection yapýlandýrmasý
builder.Services.ContainerDependencies();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.CustomerValidator();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

// Authentication ve Authorization
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/SignIn/";
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata sayfasý yapýlandýrmasý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Hata sayfalarýný yönlendirme
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[] { "tr", "en", "fr", "es", "gr", "de" }; // Türkçe ilk sýraya alýndý
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0]) // Varsayýlan dil Türkçe olarak ayarlandý
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
    .AddInitialRequestCultureProvider(new CookieRequestCultureProvider()); // Cookie tabanlý dil yönetimi
app.UseRequestLocalization(localizationOptions);


// Area ve varsayýlan rotalarý ekleme
app.UseEndpoints(endpoints =>
{
    // Area yönlendirmesi
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Admin area yetkilendirmesi
    endpoints.MapAreaControllerRoute(
        name: "admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

    // Member area yetkilendirmesi
    endpoints.MapAreaControllerRoute(
        name: "member",
        areaName: "Member",
        pattern: "Member/{controller=Home}/{action=Index}/{id?}");

    // Varsayýlan yönlendirme
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Default}/{action=Index}/{id?}");
});

app.Run();
