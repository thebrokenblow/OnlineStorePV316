using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;

namespace OnlineStore.Application.Products.Commands.ProductUpdate;

public class ProductUpdateHandler(IRepositoryProduct repository)
{
    public async Task Execute(Product product)
    {
        //Валидация
        await repository.UpdateAsync(product);
    }
}