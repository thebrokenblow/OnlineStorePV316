using OnlineStore.Model;
using OnlineStore.VM;

namespace OnlineStore.Data.Repositories.Interfaces;

public interface IRepositoryProduct
{
    /// <summary>
    /// Получение диталей продукта по его id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ProductDetailsVM> GetByIdAsync(int id);
    Task<List<Product>> GetRangeAsync(int skip, int take);
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task RemoveAsync(int id);
    Task UpdateAsync(Product updatedProduct);
}