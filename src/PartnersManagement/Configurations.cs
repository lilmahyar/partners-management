using System;
using System.Collections.Generic;
using System.Reflection;
using Ben.Diagnostics;
using BuildingBlocks.Caching;
using BuildingBlocks.Exception;
using BuildingBlocks.Logging;
using BuildingBlocks.Persistence;
using BuildingBlocks.Validation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PartnersManagement.Data;
using PartnersManagement.Orders;
using PartnersManagement.Orders.Entities;

namespace PartnersManagement
{
    public static class Configurations
    {
        public static IServiceCollection AddPartnersManagement(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<PartnerManagementDbContext>(options =>
                    options.UseInMemoryDatabase("OrdersDB"));
            }
            else
            {
                services.AddDbContext<PartnerManagementDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("OrdersConnection"),
                        b => b.MigrationsAssembly(typeof(PartnerManagementDbContext).Assembly.FullName)));
            }

            services.AddCustomValidators(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(InvalidateCachingBehavior<,>));

            services.AddCachingRequestPolicies(new List<Assembly>
            {
                Assembly.GetExecutingAssembly()
            });
            services.AddEasyCaching(options => { options.UseInMemory(configuration, "mem"); });

            services.AddProblemDetails(x =>
            {
                // Control when an exception is included
                x.IncludeExceptionDetails = (ctx, _) =>
                {
                    // Fetch services from HttpContext.RequestServices
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
                x.Map<AppException>(ex => new ProblemDetails
                {
                    Title = "Application rule broken",
                    Status = StatusCodes.Status409Conflict,
                    Detail = ex.Message,
                    Type = "https://somedomain/application-rule-validation-error",
                });
                // Exception will produce and returns from our FluentValidation RequestValidationBehavior
                x.Map<ValidationException>(ex => new ProblemDetails
                {
                    Title = "input validation rules broken",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonConvert.SerializeObject(ex.ValidationResultModel.Errors),
                    Type = "https://somedomain/input-validation-rules-error",
                });
                x.Map<BadRequestException>(ex => new ProblemDetails
                {
                    Title = "bad request exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/bad-request-error",
                });
                x.Map<NotFoundException>(ex => new ProblemDetails
                {
                    Title = "not found exception",
                    Status = StatusCodes.Status404NotFound,
                    Detail = ex.Message,
                    Type = "https://somedomain/not-found-error",
                });
                x.Map<ApiException>(ex => new ProblemDetails
                {
                    Title = "api server exception",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Type = "https://somedomain/api-server-error",
                });
                x.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
            });

            return services;
        }

        public static IApplicationBuilder UsePartnersManagement(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }

            //https://codeopinion.com/detecting-sync-over-async-code-in-asp-net-core/
            app.UseBlockingDetection();
            app.UseProblemDetails();

            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            if (configuration.GetValue<bool>("UseInMemoryDatabase") == false)
            {
                MigrateDatabase(app.ApplicationServices);
                SeedData(app.ApplicationServices);
            }

            if (env.IsEnvironment("test") == false)
                SeedData(app.ApplicationServices);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<PartnerManagementDbContext>();
            context.Database.Migrate();
        }

        private static void SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
            foreach (var seeder in seeders)
            {
                seeder.SeedAllAsync().GetAwaiter().GetResult();
            }
        }

        public static IEndpointRouteBuilder UsePartnersManagementEndpoints(this IEndpointRouteBuilder endpoints)
        {
            //endpoints.UseProductsEndpoints();

            return endpoints;
        }

        public static void ConfigureOrdersDataModel(this ModelBuilder builder)
        {
            builder.Entity<Order>().ToTable("Order", "dbo");
            builder.Entity<Order>().HasKey(x => x.Id);

            builder.Entity<Order>().HasMany(x => x.OrderItems).WithOne(x => x.Order);

            builder.Entity<Order>().Property(x => x.Partner)
                .HasConversion(
                    v => v.ToString(),
                    v => (PartnerType)Enum.Parse(typeof(PartnerType), v));
        }
    }
}