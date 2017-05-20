using Autofac;
using Autofac.Extensions.DependencyInjection;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DrinkUp.WebApi {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IContainer Container { get; set; }
        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddCors();
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<IdentityContext>(options => {
                    options.UseSqlServer(ConfigrationProvider.GetIdentityConnection(Configuration));                    
                });
            services.AddIdentity<IdentityUser, IdentityRole>();
            var builder = new ContainerBuilder();

            builder.Register(c => new MongoContext(
                    ConfigrationProvider.GetMongoConnection(Configuration),
                    ConfigrationProvider.GetMongoCollection(Configuration)))
                .AsImplementedInterfaces();
            builder.RegisterType<SearchService>().AsImplementedInterfaces();
            builder.RegisterType<DrinkService>().AsImplementedInterfaces();
            builder.RegisterType<ResponseService>().AsImplementedInterfaces();
            builder.Populate(services);

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime) {
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}