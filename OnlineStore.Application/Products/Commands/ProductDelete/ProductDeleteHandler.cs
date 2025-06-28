using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.Products.Commands.ProductDelete;

public class ProductDeleteHandler(IRepositoryProduct repository)
{
    public async Task Execute(int id)
    {
        await repository.RemoveAsync(id);
    }
}