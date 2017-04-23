using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DrinkUp.WebApi {
    public class Startup {
        public Startup() {
            var builder = new ConfigurationBuilder();

            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            app.UseMvcWithDefaultRoute();
        }
    }
}
