using Microsoft.AspNetCore.Builder;

namespace TaxAssistant.JPK.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Configuration["ApplicationSettings:JPK:Url"]) });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
