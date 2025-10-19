using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Core.Interfaces.Services;
using WebLibCs.Infrastructure.Data;
using WebLibCs.Infrastructure.Repositories;
using WebLibCs.Infrastructure.Services;

namespace WebLibCs.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register repositories
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<ILibroRepository, LibroRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register services
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IImageService, ImageService>();

        return services;
    }
}