using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.ProductCategories.Queries.GetAllProductCategories;
using OnlineStore.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineStore.Application.ProductCategories.Queries.GetRangeProductsCategories;
using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;
using OnlineStore.Storage.Data;
using OnlineStore.Storage.Exceptions;

namespace OnlineStore.Storage.Repositories;

public class RepositoryProductCategory(OnlineStoreDBContext context) : IRepositoryProductCategory
{
    public Task<GetDetailsProductCategoryVM> GetByIdAsync(int id)
    {
        var productCategories = context.ProductCategories
                                                    .Select(productCategory => new GetDetailsProductCategoryVM
                                                    { 
                                                        Id = productCategory.Id,
                                                        Name = productCategory.Name,
                                                        Description = productCategory.Description
                                                    })
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(productCategories => productCategories.Id == id);

        return productCategories;
    }

    public Task<List<GetRangeProductCategoriesVM>> GetRangeAsync(int skip, int take)
    {
        var productCategories = context.ProductCategories
                                                    .Select(productCategory => new GetRangeProductCategoriesVM
                                                    { 
                                                        Id = productCategory.Id,
                                                        Name = productCategory.Name,
                                                    })
                                                    .AsNoTracking()
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToListAsync();

        return productCategories;
    }

    public Task<List<GetAllProductCategoryVM>> GetAllAsync()
    {
        var productCategories = context.ProductCategories
                                                    .Select(productCategory => new GetAllProductCategoryVM
                                                    {
                                                        Id = productCategory.Id,
                                                        Name = productCategory.Name,
                                                    })
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