using OnlineStore.Model;

namespace OnlineStore.Data.Repositories.Interfaces;

public interface IRepositoryProduct
{
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> GetRangeAsync(int skip, int take);
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task RemoveAsync(int id);
    Task UpdateAsync(Product updatedProduct);
}