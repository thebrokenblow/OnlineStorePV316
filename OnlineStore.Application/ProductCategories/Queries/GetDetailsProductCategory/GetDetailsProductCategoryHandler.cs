using OnlineStore.Application.RepositoryInterfaces;

namespace OnlineStore.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class GetDetailsProductCategoryHandler(IRepositoryProductCategory repository)
{
    public async Task<GetDetailsProductCategoryVM> Execute(int id)
    {
        //Валидация Guid 
        return await repository.GetByIdAsync(id);  
    }
}
