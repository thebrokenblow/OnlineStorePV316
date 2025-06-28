using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.ProductCategories.Queries.GetRangeProductsCategories;

public class GetRangeProductCategoriesHandler(IRepositoryProductCategory repository)
{
    public async Task<List<GetRangeProductCategoriesVM>> Execute(int skip, int take)
    {
        //Валидация skip >= 0, take >= 0
        return await repository.GetRangeAsync(skip, take);
    }
}