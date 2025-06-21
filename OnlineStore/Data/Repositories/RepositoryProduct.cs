using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Model;

namespace OnlineStore.Data.Repositories;

public class RepositoryProduct(OnlineStoreDBContext context) : IRepositoryProduct
{
    public Task<Product> GetByIdAsync(int id)
    {
        var product = context.Products
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(product => product.Id == id);

        return product;
    }

    public Task<List<Product>> GetRangeAsync(int skip, int take)
    {
        var products = context.Products
                                            .AsNoTracking()
                                            .Skip(skip)
                                            .Take(take)
                                            .ToListAsync();

        return products;
    }

    public Task<List<Product>> GetAllAsync()
    {
        var products = context.Products
                                    .AsNoTracking()
                                    .ToListAsync();

        return products;
    }

    public async Task AddAsync(Product product)
    {
        await context.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var product = await GetByIdTrackingAsync(id);

        context.Products.Remove(product);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product updatedProduct)
    {
        var product = await GetByIdTrackingAsync(updatedProduct.Id);

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.ProductCategoryId = updatedProduct.ProductCategoryId;

        await context.SaveChangesAsync();
    }

    private async Task<Product> GetByIdTrackingAsync(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(
                productCategory => productCategory.Id == id);

        return product;
    }
}