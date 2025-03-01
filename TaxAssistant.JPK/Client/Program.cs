using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxAssistant.JPK.Client;
using TaxAssistant.JPK.Client.Clients;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IApiClient<Kpir>, KpirClient>();
builder.Services.AddScoped<IApiClient<Ewp>, EwpClient>();
builder.Services.AddScoped<IApiClient<Fa>, FaClient>();
builder.Services.AddScoped<IApiClient<Import>, ImportClient>();
builder.Services.AddScoped<IApiClient<Company>, CompanyClient>();

await builder.Build().RunAsync();
