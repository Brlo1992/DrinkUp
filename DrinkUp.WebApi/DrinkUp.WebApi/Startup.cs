﻿using Autofac;
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
using Microsoft.Extensions.Logging;
using System;

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
                .AddDbContext<AuthorizationContext>(options => {
                    options.UseSqlServer(ConfigrationProvider.GetIdentityConnection(Configuration));
                });
            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuthorizationContext>()
                .AddDefaultTokenProviders();
            var builder = new ContainerBuilder();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

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