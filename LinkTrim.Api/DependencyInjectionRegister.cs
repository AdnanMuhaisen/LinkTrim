using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using LinkTrim.Api.Core.Interfaces;
using LinkTrim.Api.Infrastructure.Data.Contexts;
using LinkTrim.Api.Infrastructure.Services;
using LinkTrim.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Api;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        });

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly);
        });

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(DependencyInjectionRegister).Assembly);

        services.AddScoped<IUrlMappingMapper, UrlMappingMapper>();
        services.AddScoped<IUrlHashingService, UrlHashingService>();

        return services;
    }
}