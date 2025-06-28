using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Products.Queries.GetAllProducts;
using OnlineStore.Application.Products.Queries.GetDetailsProduct;
using OnlineStore.Application.Products.Queries.GetRangeProducts;
using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;
using OnlineStore.Storage.Data;
using OnlineStore.Storage.Exceptions;

namespace OnlineStore.Storage.Repositories;

public class RepositoryProduct(OnlineStoreDBContext context) : IRepositoryProduct
{
    /// <summary>
    /// Получение деталей продукта по его id с помощью Entity Framework
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<GetDetailsProductVM> GetByIdAsync(int id)
    {
        var productDetailsVM = await context.Products
                                                .AsNoTracking()
                                                .Include(product => product.ProductCategory)
                                                .Select(product => new GetDetailsProductVM
                                                { 
                                                    Id = product.Id,
                                                    Name = product.Name,
                                                    Description = product.Description,
                                                    Price = product.Price,
                                                    DetailsProductCategoryVM = new GetDetailsProductCategoryVM
                                                    {
                                                        Id = product.ProductCategoryId,
                                                        Name = product.ProductCategory!.Name,
                                                        Descrition = product.ProductCategory!.Description,
                                                    }
                                                })
                                                .FirstOrDefaultAsync(product => product.Id == id)
                                                ?? throw new NotFoundException($"Entity {nameof(Product)} not found by id {id}");

        return productDetailsVM;
    }


    public async Task<List<GetRangeProductsVM>> GetRangeAsync(int skip, int take)
    {
        var products = await context.Products
                                        .Include(product => product.ProductCategory)
                                        .Select(product => new GetRangeProductsVM
                                        { 
                                            Id = product.Id,
                                            Name = product.Name,
                                            Price = product.Price,
                                            NameProductCategory = product.ProductCategory!.Name
                                        })
                                        .AsNoTracking()
                                        .Skip(skip)
                                        .Take(take)
                                        .ToListAsync();

        return products;
    }

    
    public async Task<List<GetAllProductsVM>> GetAllAsync()
    {
        var products = await context.Products
                                    .Include(product => product.ProductCategory)
                                    .Select(product => new GetAllProductsVM
                                    { 
                                        Id = product.Id,
                                        Name = product.Name,
                                        Price = product.Price,
                                        NameProductCategory = product.ProductCategory!.Name
                                    })
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