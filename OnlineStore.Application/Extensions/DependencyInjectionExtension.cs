using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.ProductCategories.Queries.GetAllProductCategories;

namespace OnlineStore.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        services.AddScoped<GetAllProductCategoriesHandler>();

        return services;
    }
}