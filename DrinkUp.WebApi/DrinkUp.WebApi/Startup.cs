using Autofac;
using Autofac.Extensions.DependencyInjection;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model.Identity;
using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddCors(options => { options.AddPolicy("dev", GetCorsPolicy()); });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });


            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<AuthorizationContext>(options => {
                    options.UseSqlServer(ConfigrationProvider.GetIdentityConnection(Configuration));
                });
            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuthorizationContext>()
                .AddDefaultTokenProviders();

            var builder = new ContainerBuilder();

            services.Configure(ConfigureIdentityOptions());

            builder.Register(c => new MongoContext(
                    ConfigrationProvider.GetMongoConnection(Configuration),
                    ConfigrationProvider.GetMongoCollection(Configuration)))
                .AsImplementedInterfaces();
            builder.RegisterType<SearchService>().AsImplementedInterfaces();
            builder.RegisterType<DrinkService>().AsImplementedInterfaces();
            builder.RegisterType<ResponseService>().AsImplementedInterfaces();
            builder.RegisterType<AccountService>().AsImplementedInterfaces();
            builder.Populate(services);

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        private Action<IdentityOptions> ConfigureIdentityOptions() {
            return options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                // User settings
                options.User.RequireUniqueEmail = true;
            };
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime) {
            app.UseCookieAuthentication();
            app.UseIdentity();
            app.UseCors("dev");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            appLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }

        private CorsPolicy GetCorsPolicy() {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowCredentials();
            corsBuilder.AllowAnyOrigin();
            return corsBuilder.Build();
        }
    }
}