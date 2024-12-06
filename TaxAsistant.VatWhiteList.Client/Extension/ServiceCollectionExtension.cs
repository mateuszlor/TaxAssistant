using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxAsistant.VatWhiteList.Client.Client;
using TaxAsistant.VatWhiteList.Client.Configuration;

namespace TaxAsistant.VatWhiteList.Client.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddVatWhiteListClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VatWhiteListConfiguration>(configuration.GetSection("VatWhiteList"));
            services.AddHttpClient();
            services.AddScoped<IVatWhiteListClient, VatWhiteListClient>();
        }
    }
}
