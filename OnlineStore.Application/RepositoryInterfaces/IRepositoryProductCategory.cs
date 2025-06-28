using OnlineStore.Application.ProductCategories.Queries.GetAllProductCategories;
using OnlineStore.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineStore.Application.ProductCategories.Queries.GetRangeProductsCategories;
using OnlineStore.Domain;

namespace OnlineStore.Application.RepositoryInterfaces;

public interface IRepositoryProductCategory
{
    Task<GetDetailsProductCategoryVM> GetByIdAsync(int id);
    Task<List<GetRangeProductCategoriesVM>> GetRangeAsync(int skip, int take);
    Task<List<GetAllProductCategoryVM>> GetAllAsync();
    Task AddAsync(ProductCategory productCategory);
    Task RemoveAsync(int id);
    Task UpdateAsync(ProductCategory updatedProductCategory);
}