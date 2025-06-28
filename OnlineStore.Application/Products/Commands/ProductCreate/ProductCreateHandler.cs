using FluentValidation;
using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;

namespace OnlineStore.Application.Products.Commands.ProductCreate;

public class ProductCreateHandler(IRepositoryProduct repository, ProductCreateValidation productCreateValidation)
{
    public async Task Execute(Product product)
    {
        productCreateValidation.ValidateAndThrow(product);

        await repository.AddAsync(product);
    }
}