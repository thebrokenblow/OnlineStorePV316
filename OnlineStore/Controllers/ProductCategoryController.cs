using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.ProductCategories.Commands.ProductCategoryCreate;
using OnlineStore.Application.ProductCategories.Commands.ProductCategoryDelete;
using OnlineStore.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineStore.Application.ProductCategories.Queries.GetAllProductCategories;
using OnlineStore.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineStore.Application.ProductCategories.Queries.GetRangeProductsCategories;
using OnlineStore.Application.RepositoryInterfaces;
using OnlineStore.Domain;
using OnlineStore.Storage.Exceptions;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/categories")]
public class ProductCategoryController(IRepositoryProductCategory repository) : Controller
{
    [HttpGet]
    public async Task<ActionResult> GetAllAsync([FromServices] GetAllProductCategoriesHandler getAllProductCategoriesHandler)
    {
        var productCategories = await getAllProductCategoriesHandler.Execute();

        return Ok(productCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var getDetailsProductCategoryHandler = new GetDetailsProductCategoryHandler(repository);
        var productCategory = await getDetailsProductCategoryHandler.Execute(id);

        return Ok(productCategory);
    }

    [HttpGet("{skip}/{take}")]
    public async Task<ActionResult> GetRangeAsync(int skip, int take)
    {
        var getRangeProductCategoriesHandler = new GetRangeProductCategoriesHandler(repository);
        var productCategories = await getRangeProductCategoriesHandler.Execute(skip, take);

        return Ok(productCategories);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(ProductCategory productCategory)
    {
        var createProductCategoryHandler = new CreateProductCategoryHandler(repository);
        await createProductCategoryHandler.Execute(productCategory);

        return Created();
    }

    [HttpDelete("{id}")] 
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleteProductCategoryHandler = new DeleteProductCategoryHandler(repository);
            await deleteProductCategoryHandler.Execute(id);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(ProductCategory productCategory)
    {
        try
        {
            var updateProductCategoryHandler = new UpdateProductCategoryHandler(repository);
            await updateProductCategoryHandler.Execute(productCategory);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }
}