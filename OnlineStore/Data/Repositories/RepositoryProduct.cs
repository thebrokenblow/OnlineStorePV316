using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exceptions;
using OnlineStore.Model;
using OnlineStore.VM;

namespace OnlineStore.Data.Repositories;

public class RepositoryProduct(OnlineStoreDBContext context) : IRepositoryProduct
{
    /// <summary>
    /// Получение диталей продукта по его id с помощью Entity Framework
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<ProductDetailsVM> GetByIdAsync(int id)
    {
        var productDetailsVM = await context.Products
                                                .AsNoTracking()
                                                .Include(product => product.ProductCategory)
                                                .Select(product => new ProductDetailsVM
                                                { 
                                                    Id = product.Id,
                                                    Name = product.Name,
                                                    Description = product.Description,
                                                    Price = product.Price,
                                                    ProductDetailsCategoryVM = new ProductDetailsCategoryVM
                                                    {
                                                        IdProductCategory = product.ProductCategoryId,
                                                        NameProductCategory = product.ProductCategory!.Name,
                                                        DescriptionProductCategory = product.ProductCategory!.Description,
                                                    }
                                                })
                                                .FirstOrDefaultAsync(product => product.Id == id)
                                                ??throw new NotFoundException($"Entity {nameof(Product)} not found by id {id}");

        return productDetailsVM;
    }


    public async Task<List<Product>> GetRangeAsync(int skip, int take)
    {
        //Id, Name, Price, NameCategory
        //Select
        var products = await context.Products
                                    .AsNoTracking()
                                    .Skip(skip)
                                    .Take(take)
                                    .ToListAsync();

        return products;
    }

    
    public async Task<List<Product>> GetAllAsync()
    {
        //Select
        //Name, Price, NameCategory
        var products = await context.Products
                                    .AsNoTracking()
                                    .ToListAsync();

        return products;
    }

    public async Task AddAsync(Product product)
    {
        var isExist = await context.ProductCategories
                                                .AnyAsync(productCategory => productCategory.Id == product.ProductCategoryId);

        if (!isExist)
        {
            throw new NotFoundException($"Entity {nameof(ProductCategory)} not found by id {product.ProductCategoryId}");
        }

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
        var productCategory = await context.Products.FirstOrDefaultAsync(
                product => product.Id == id) ??
            throw new NotFoundException($"Entity {nameof(Product)} not found by id {id}");

        return productCategory;
    }
}