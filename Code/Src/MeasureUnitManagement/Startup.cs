using MeasureUnitManagement.Application.Handlers;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using MeasureUnitManagement.Infrastructure.DataAccess;
using MeasureUnitManagement.Infrastructure.Persistence;
using MeasureUnitManagement.Infrastructure.Persistence.Mappings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace MeasureUnitManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddMvcOptions(x =>
                {
                    x.EnableEndpointRouting = false;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Measure unit management Api", Version = "v1" });
            });

            services.AddMongoDB(this.Configuration);
            services.AddDomainServices();
            services.AddRepositories();
            services.AddFactories();
            services.AddCommandHandlers();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHealthChecks("/health");
            app.UseMvc();
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

        public static IServiceCollection AddMongoDB(this IServiceCollection services,
            IConfiguration configuration)
        {
            new MeasureDimensionMappings().Register();
            services.AddTransient<IMongoDatabase>((s) =>
            {
                var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
                return client.GetDatabase(configuration["MongoDB:DbName"]);
            });
            services.AddTransient<ISequenceIdGenerator, MongoSequenceIdGenerator>();
            services.AddTransient(typeof(IDocumentBasedRepository<,>), typeof(MongoRepository<,>));
            return services;
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(Startup).Assembly);
        }
    }
}
