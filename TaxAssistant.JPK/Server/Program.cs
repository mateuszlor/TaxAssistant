using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Proxies;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Server;
using TaxAssistant.JPK.Shared.Adapter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<KpirAdapter>();
builder.Services.AddScoped<KpirRepository>();
builder.Services.AddScoped<ImportRepository>(); 

var connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<DatabaseContext>(x => x
                .EnableSensitiveDataLogging()
                .ConfigureWarnings(x => x.Log(CoreEventId.DetachedLazyLoadingWarning))
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.EnsureDatabaseMigration();

app.Run();

