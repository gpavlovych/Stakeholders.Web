﻿// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Startup.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AutoMapper.QueryableExtensions;
using Microsoft.DotNet.Cli.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Services;
using AutoMapper;
using Stakeholders.Web.Models.ActivityTaskStatusViewModels;
using Stakeholders.Web.Models.ActivityTypeViewModels;
using Stakeholders.Web.Models.ActivityViewModels;
using Stakeholders.Web.Models.CompanyViewModels;
using Stakeholders.Web.Models.OrganizationTypeViewModels;

namespace Stakeholders.Web
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(this.Configuration);

            services.AddDbContext<ApplicationDbContext>(
                options =>
                        options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDataSource<Activity>, ActivityDataSource>();
            services.AddScoped<IDataSource<ActivityTask>, ActivityTaskDataSource>();
            services.AddScoped<IDataSource<ActivityTaskStatus>, ActivityTaskStatusDataSource>();
            services.AddScoped<IDataSource<ActivityType>, ActivityTypeDataSource>();
            services.AddScoped<IDataSource<Company>, CompanyDataSource>();
            services.AddScoped<IDataSource<Contact>, ContactDataSource>();
            services.AddScoped<IDataSource<Goal>, GoalDataSource>();
            services.AddScoped<IDataSource<Organization>, OrganizationDataSource>();
            services.AddScoped<IDataSource<OrganizationType>, OrganizationTypeDataSource>();
            services.AddScoped<IDataSource<OrganizationCategory>, OrganizationCategoryDataSource>();
            services.AddScoped<IDataSource<ApplicationUser>, ApplicationUserDataSource>();
            services.AddScoped<IDataSource<Role>, RoleDataSource>();

            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext, long>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddAutoMapper(
                mapperConfigurationExpression =>
                {
                    mapperConfigurationExpression.CreateMap<ActivityTaskStatus, ActivityTaskStatusViewModel>();
                    mapperConfigurationExpression.CreateMap<ActivityTaskStatusViewModel, ActivityTaskStatus>();
                    mapperConfigurationExpression.CreateMap<ActivityTaskStatus, ActivityTaskStatusInfoViewModel>();

                    mapperConfigurationExpression.CreateMap<ActivityType, ActivityTypeViewModel>();
                    mapperConfigurationExpression.CreateMap<ActivityTypeViewModel, ActivityType>();
                    mapperConfigurationExpression.CreateMap<ActivityType, ActivityTypeInfoViewModel>();

                    mapperConfigurationExpression.CreateMap<OrganizationType, OrganizationTypeViewModel>();
                    mapperConfigurationExpression.CreateMap<OrganizationTypeViewModel, OrganizationType>();
                    mapperConfigurationExpression.CreateMap<OrganizationType, OrganizationTypeInfoViewModel>();

                    mapperConfigurationExpression.CreateMap<Company, CompanyInfoViewModel>()
                        .ForMember(it => it.ObserverActivityIds, c => c.ResolveUsing<ObserverActivitiesToViewModelResolver>());
                    mapperConfigurationExpression.CreateMap<CompanyViewModel, Company>()
                        .ForMember(it => it.ObserverActivities, c => c.ResolveUsing<ObserverActivitiesToEntityResolver>());
                    mapperConfigurationExpression.CreateMap<Company, CompanyViewModel>()
                        .ForMember(it => it.ObserverActivityIds, c => c.ResolveUsing<ObserverActivitiesToViewModelResolver>());

                    //cfg.CreateMap<Activity, ActivityInfoViewModel>().ForMember(
                    //      it => it.ObserverCompanyIds,
                    //      c => c.ResolveUsing<ObserverCompanyIdsToViewModelResolver>());
                    //cfg.CreateMap<ActivityViewModel, Activity>().ForMember(
                    //        it => it.ObserverActivities,
                    //        c => c.ResolveUsing<ObserverActivitiesToEntityResolver>());
                    //cfg.CreateMap<Activity, ActivityViewModel>()
                    //    .ForMember(
                    //        it => it.ObserverCompanyIds,
                    //        c => c.ResolveUsing<ObserverActivitiesToViewModelResolver>());
                });
        }

        // The secret key every token will be signed with.
        // In production, you should store this securely in environment variables
        // or a key management tool. Don't hardcode this into your application!
        private static readonly string secretKey = "mysupersecret_secretkey!123";

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();

                using (
                    var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().EnsureSeedData();
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            // Add JWT generation endpoint:
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var options = new TokenProviderOptions
            {
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(
                new JwtBearerOptions
                {
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    TokenValidationParameters = tokenValidationParameters
                });

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }

    public class ObserverCompanyIdsToViewModelResolver
    {
    }

    public class ObserverActivitiesToEntityResolver
        : IValueResolver<CompanyViewModel, Company, ICollection<ActivityObserverUserCompany>>
    {
        private readonly IRepository<Activity> repositoryActivities;

        public ObserverActivitiesToEntityResolver(IRepository<Activity> repositoryActivities)
        {
            this.repositoryActivities = repositoryActivities;
        }

        public ICollection<ActivityObserverUserCompany> Resolve(
            CompanyViewModel source,
            Company destination,
            ICollection<ActivityObserverUserCompany> destMember,
            ResolutionContext context)
        {
            var result = source.ObserverActivityIds?.Select(
                id => new ActivityObserverUserCompany()
                {
                    Activity = this.repositoryActivities.FindById(id)
                }).ToList();
            return result;
        }
    }

    public class ObserverActivitiesToViewModelResolver : IValueResolver<Company, CompanyViewModel, long[]>
    {
        public long[] Resolve(
            Company source,
            CompanyViewModel destination,
            long[] destMember,
            ResolutionContext context)
        {
            return (source.ObserverActivities?.Select(it => it.Activity?.Id ?? 0) ?? Enumerable.Empty<long>()).ToArray();
        }
    }
}
