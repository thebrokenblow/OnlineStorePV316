using OnlineStore.Model;

namespace OnlineStore.Data.Repositories.Interfaces;

public interface IRepositoryProductCategory
{
    Task<ProductCategory> GetByIdAsync(int id);
    Task<List<ProductCategory>> GetRangeAsync(int skip, int take);
    Task<List<ProductCategory>> GetAllAsync();
    Task AddAsync(ProductCategory productCategory);
    Task RemoveAsync(int id);
    Task UpdateAsync(ProductCategory updatedProductCategory);
}