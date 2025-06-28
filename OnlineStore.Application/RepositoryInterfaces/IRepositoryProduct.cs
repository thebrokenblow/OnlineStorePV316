using OnlineStore.Application.Products.Queries.GetAllProducts;
using OnlineStore.Application.Products.Queries.GetDetailsProduct;
using OnlineStore.Application.Products.Queries.GetRangeProducts;
using OnlineStore.Domain;

namespace OnlineStore.Application.RepositoryInterfaces;

public interface IRepositoryProduct
{
    /// <summary>
    /// Получение деталей продукта по его id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    Task<GetDetailsProductVM> GetByIdAsync(int id);
    Task<List<GetRangeProductsVM>> GetRangeAsync(int skip, int take);
    Task<List<GetAllProductsVM>> GetAllAsync();
    Task AddAsync(Product product);
    Task RemoveAsync(int id);
    Task UpdateAsync(Product updatedProduct);
}