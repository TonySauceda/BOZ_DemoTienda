using DemoTienda.Application.Interfaces;
using DemoTienda.Infraestructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DemoTienda.Infraestructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        return services;
    }
}
