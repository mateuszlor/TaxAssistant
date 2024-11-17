using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaxAssistant.CQRS;
using TaxAssistant.DDD;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Server;
using TaxAssistant.JPK.Shared.Adapter;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

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
builder.Services.AddScoped<IRepository<Kpir>, KpirRepository>();
builder.Services.AddScoped<EwpAdapter>();
builder.Services.AddScoped<IRepository<Ewp>, EwpRepository>();
builder.Services.AddScoped<FaAdapter>();
builder.Services.AddScoped<IRepository<Fa>, FaRepository>();
builder.Services.AddScoped<IRepository<Import>, ImportRepository>();

builder.Services.AddCqrs();
builder.Services.AddCommandHandlers();
builder.Services.AddDDD();
builder.Services.AddDomainEventHandlers();

builder.Services.AddSwaggerGen();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.EnsureDatabaseMigration();

app.Run();

