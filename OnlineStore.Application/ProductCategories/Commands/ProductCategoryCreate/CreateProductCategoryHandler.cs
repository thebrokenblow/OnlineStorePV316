using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;

namespace OnlineStore.Application.ProductCategories.Commands.ProductCategoryCreate;

public class CreateProductCategoryHandler(IRepositoryProductCategory repository)
{
    public async Task Execute(ProductCategory productCategory)
    {
        await repository.AddAsync(productCategory);
    }
}