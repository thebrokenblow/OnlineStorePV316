using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.Products.Queries.GetRangeProducts;

public class GetRangeProductsHandler(IRepositoryProduct repository)
{
    public async Task<List<GetRangeProductsVM>> Execute(int skip, int take)
    {
        //Валидация skip >= 0, take >= 0
        return await repository.GetRangeAsync(skip, take);
    }
}