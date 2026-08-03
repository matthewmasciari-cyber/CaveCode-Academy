using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CaveCode;
using CaveCode.Services;
using CaveCode.CourseEngine;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AchievementService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<ProgressionService>();
builder.Services.AddScoped<MinigameService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<CourseCatalogService>();
builder.Services.AddScoped<ICourseCodeValidator, StructuralCourseCodeValidator>();
builder.Services.AddScoped<ICourseCodeValidator, HtmlCourseCodeValidator>();
builder.Services.AddScoped<CourseEngineService>();

await builder.Build().RunAsync();
