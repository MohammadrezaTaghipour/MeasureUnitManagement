using MeasureUnitManagement.Application.Handlers;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using MeasureUnitManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MeasureUnitManagement
{
    public class Startup
    {
        public Startup()
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Accounting Api", Version = "v1" });
                c.TagActionsBy(a => a.ActionDescriptor.RouteValues["controller"].Replace("Query", ""));
            });

            services.AddDomainServices();
            services.AddRepositories();
            services.AddFactories();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvcWithDefaultRoute();
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services.AddTransient<IFormulaExpressionEvaluator, FormulaExpressionEvaluator>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddTransient<IMeasureDimensionRepository, MeasureDimensionRepository>();
        }

        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            return services.AddTransient<IMeasureDimensionArgFactory, MeasureDimensionArgFactory>();
        }
    }
}
