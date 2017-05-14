using Autofac;
using Autofac.Extensions.DependencyInjection;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using DrinkUp.WebApi.Utils;
using Swashbuckle.AspNetCore.Swagger;

namespace DrinkUp.WebApi {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer Container { get; set; }
        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            var builder = new ContainerBuilder();

            builder.RegisterType<MongoContext>().AsImplementedInterfaces();
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            appLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
