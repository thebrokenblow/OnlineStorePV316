using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.ProductCategories.Commands.ProductCategoryDelete;

public class DeleteProductCategoryHandler(IRepositoryProductCategory repository)
{
    public async Task Execute(int id)
    {
        await repository.RemoveAsync(id);
    }
}