using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exceptions;
using OnlineStore.Model;

namespace OnlineStore.Data.Repositories;

public class RepositoryProductCategory(OnlineStoreDBContext context) : IRepositoryProductCategory
{
    public Task<ProductCategory> GetByIdAsync(int id)
    {
        var productCategories = context.ProductCategories
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(productCategories => productCategories.Id == id);

        return productCategories;
    }

    public Task<List<ProductCategory>> GetRangeAsync(int skip, int take)
    {
        var productCategories = context.ProductCategories
                                                    .AsNoTracking()
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToListAsync();

        return productCategories;
    }

    public Task<List<ProductCategory>> GetAllAsync()
    {
        var productCategories = context.ProductCategories
                                                    .AsNoTracking()
                                                    .ToListAsync();

        return productCategories;
    }

    public async Task AddAsync(ProductCategory productCategory)
    {
        await context.AddAsync(productCategory);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var productCategory = await GetByIdTrackingAsync(id);

        context.ProductCategories.Remove(productCategory);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductCategory updatedProductCategory)
    {
        var productCategory = await GetByIdTrackingAsync(updatedProductCategory.Id);

        productCategory.Name = updatedProductCategory.Name;
        productCategory.Description = updatedProductCategory.Description;

        await context.SaveChangesAsync();
    }

    private async Task<ProductCategory> GetByIdTrackingAsync(int id)
    {
        var productCategory = await context.ProductCategories.FirstOrDefaultAsync(
                productCategory => productCategory.Id == id) ??
            throw new NotFoundException($"Entity {nameof(ProductCategory)} not found by id {id}");

        return productCategory;
    }
}