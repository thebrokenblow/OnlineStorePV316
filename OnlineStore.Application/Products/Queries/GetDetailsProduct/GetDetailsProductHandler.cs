using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.Products.Queries.GetDetailsProduct;

public class GetDetailsProductHandler(IRepositoryProduct repository)
{
    public Task<GetDetailsProductVM> Execute(int id)
    {
        return repository.GetByIdAsync(id);
    }
}
