using BuStudentAssistant.Components;
using BuStudentAssistant.Data;
using BuStudentAssistant.Services;
using Majorsoft.Blazor.Components.Maps;
using Majorsoft.Blazor.Components.Common.JsInterop;
using BuStudentAssistant.Data;
using BuStudentAssistant.Services;
using Majorsoft.Blazor.Components.Maps;
using Majorsoft.Blazor.Components.Common.JsInterop;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<UserService>();

builder.Services.AddMapExtensions();
builder.Services.AddJsInteropExtensions();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddMapExtensions();
builder.Services.AddJsInteropExtensions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();