using LinkTrim.Api.Core.Interfaces;
using LinkTrim.Api.Infrastructure.Data.Contexts;
using LinkTrim.Api.Infrastructure.Services;
using LinkTrim.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Api;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
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

        services.AddScoped<IUrlMappingMapper, UrlMappingMapper>();
        services.AddScoped<IUrlHashingService, UrlHashingService>();

        return services;
    }
}