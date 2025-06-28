using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.ProductCategories.Queries.GetAllProductCategories;
using OnlineStore.Application.Products.Commands.ProductCreate;
using System.Reflection;

namespace OnlineStore.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

        services.AddScoped<ProductCreateHandler>();
        services.AddScoped<GetAllProductCategoriesHandler>();

        return services;
    }
}