using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ben.Diagnostics;
using BuildingBlocks.Swagger;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PartnersManagement.Core;

namespace PartnersManagement.Api
{
    public class Startup
    {
        private const string AppOptionsSectionName = "AppOptions";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appOptions = Configuration.GetSection(AppOptionsSectionName).Get<AppOptions>();
            services.AddOptions<AppOptions>().Bind(Configuration.GetSection(AppOptionsSectionName))
                .ValidateDataAnnotations();

            services.AddControllers(options =>
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("api", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            services.AddCustomHealthCheck(healthBuilder => { });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddCustomVersioning();
            services.AddCustomSwagger(Configuration, typeof(Root).Assembly);
            services.AddCustomApiKeyAuthentication();

            services.AddPartnersManagement(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("docker"))
            {
                app.UseDeveloperExceptionPage();
                var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                app.UseCustomSwagger(provider);
            }

            app.UsePartnersManagement(env);

            app.UseCors("api");

            app.UseRouting();

            app.UseCustomHealthCheck();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.UsePartnersManagementEndpoints();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Partners Management APIs!"));
            });
        }
    }
}