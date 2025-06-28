using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Products.Commands.ProductCreate;
using OnlineStore.Application.Products.Commands.ProductDelete;
using OnlineStore.Application.Products.Commands.ProductUpdate;
using OnlineStore.Application.Products.Queries.GetAllProducts;
using OnlineStore.Application.Products.Queries.GetDetailsProduct;
using OnlineStore.Application.Products.Queries.GetRangeProducts;
using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Dto;
using OnlineStore.Mappers;
using OnlineStore.Storage.Exceptions;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IRepositoryProduct repository) : Controller
{
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var getAllProductsHandler = new GetAllProductsHandler(repository);
        var products = await getAllProductsHandler.Execute();

        return Ok(products);
    }

    [HttpGet("{skip}/{take}")]
    public async Task<ActionResult> GetRangeAsync(int skip, int take)
    {
        var getRangeProductsHandler = new GetRangeProductsHandler(repository);
        var products = await getRangeProductsHandler.Execute(skip, take);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var getDetailsProductHandler = new GetDetailsProductHandler(repository);
        var product = await getDetailsProductHandler.Execute(id);

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(ProductDto productDto)
    {
        try
        {
            var product = MapperProductDto.Map(productDto);

            var productCreateHandler = new ProductCreateHandler(repository);
            await productCreateHandler.Execute(product);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var productDeleteHandler = new ProductDeleteHandler(repository);
            await productDeleteHandler.Execute(id);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(ProductDto productDto)
    {
        try
        {
            
            var product = MapperProductDto.Map(productDto);

            var productUpdateHandler = new ProductUpdateHandler(repository);
            await productUpdateHandler.Execute(product);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }
}