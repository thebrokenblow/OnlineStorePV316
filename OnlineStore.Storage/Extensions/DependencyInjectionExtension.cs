using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Storage.Data;
using OnlineStore.Storage.Repositories;

namespace OnlineStore.Storage.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddStorageDependency(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OnlineStoreDBContext>(options => 
                                                        options.UseNpgsql(connectionString));

        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IRepositoryProductCategory, RepositoryProductCategory>();

        return services;
    }
}