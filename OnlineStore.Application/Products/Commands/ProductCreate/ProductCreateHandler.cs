using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;

namespace OnlineStore.Application.Products.Commands.ProductCreate;

public class ProductCreateHandler(IRepositoryProduct repository)
{
    public async Task Execute(Product product)
    {
        //Валидация product
        await repository.AddAsync(product);
    }
}
