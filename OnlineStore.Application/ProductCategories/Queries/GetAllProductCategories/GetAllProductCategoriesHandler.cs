using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.ProductCategories.Queries.GetAllProductCategories;

public class GetAllProductCategoriesHandler(IRepositoryProductCategory repository)
{
    public async Task<List<GetAllProductCategoryVM>> Execute()
    {
        return await repository.GetAllAsync();
    }
}