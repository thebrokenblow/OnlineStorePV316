using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exceptions;
using OnlineStore.Model;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController(IRepositoryProductCategory repository) : Controller
{
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var productCategories = await repository.GetAllAsync();

        return Ok(productCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var productCategory = await repository.GetByIdAsync(id);

        return Ok(productCategory);
    }

    [HttpGet("{skip}/{take}")]
    public async Task<ActionResult> GetRangeAsync(int skip, int take)
    {
        var productCategories = await repository.GetRangeAsync(skip, take);

        return Ok(productCategories);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(ProductCategory productCategory)
    {
        await repository.AddAsync(productCategory);

        return Created();
    }

    [HttpDelete("{id}")] 
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await repository.RemoveAsync(id);
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
            await repository.UpdateAsync(productCategory);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }
}