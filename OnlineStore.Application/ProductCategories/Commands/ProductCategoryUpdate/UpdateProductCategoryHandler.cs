using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;

namespace OnlineStore.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryHandler(IRepositoryProductCategory repository)
{
    public async Task Execute(ProductCategory productCategory)
    {
        //Валидация
        await repository.UpdateAsync(productCategory);
    }
}