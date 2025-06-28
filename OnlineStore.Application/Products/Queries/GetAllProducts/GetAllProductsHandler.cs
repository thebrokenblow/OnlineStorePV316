using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.Products.Queries.GetAllProducts;

public class GetAllProductsHandler(IRepositoryProduct repository)
{
    public async Task<List<GetAllProductsVM>> Execute()
    {
       return await repository.GetAllAsync();
    }
}