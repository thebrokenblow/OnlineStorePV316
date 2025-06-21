using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Repositories.Interfaces;
using OnlineStore.Exceptions;
using OnlineStore.Model;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IRepositoryProduct repository) : Controller
{
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var products = await repository.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var product = await repository.GetByIdAsync(id);

        return Ok(product);
    }

    [HttpGet("{skip}/{take}")]
    public async Task<ActionResult> GetRangeAsync(int skip, int take)
    {
        var products = await repository.GetRangeAsync(skip, take);

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(Product product)
    {
        await repository.AddAsync(product);

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
    public async Task<ActionResult> UpdateAsync(Product product)
    {
        try
        {
            await repository.UpdateAsync(product);
        }
        catch (NotFoundException)
        {
            throw;
        }

        return NoContent();
    }
}