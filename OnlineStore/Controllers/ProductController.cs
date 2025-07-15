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
    /// <summary>
    /// Gets the all products
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products
    /// </remarks>
    /// <returns>Returns list of allProductDto</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllAsync()
    {
        var getAllProductsHandler = new GetAllProductsHandler(repository);
        var products = await getAllProductsHandler.Execute();

        return Ok(products);
    }

    /// <summary>
    /// Gets the products from the range
    /// </summary>
    /// <remarks>
    /// <param name="skip">count of products to skip (int)</param>
    /// <param name="take">count of products to take (int)</param>
    /// Sample request:
    /// GET /api/products/0/10
    /// </remarks>
    /// <returns>Returns list of getRangeProductsVM</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If countSkip is less than zero or countTake is less than or equal to zero</response>
    [HttpGet("{skip}/{take}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetRangeAsync(int skip, int take)
    {
        var getRangeProductsHandler = new GetRangeProductsHandler(repository);
        var products = await getRangeProductsHandler.Execute(skip, take);

        return Ok(products);
    }

    /// <summary>
    /// Gets the details product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products/1
    /// </remarks>
    /// <param name="id">Product id (int)</param>
    /// <returns>Returns detailsProductDto</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If product is not found by id</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var getDetailsProductHandler = new GetDetailsProductHandler(repository);
        var product = await getDetailsProductHandler.Execute(id);

        return Ok(product);
    }

    /// <summary>
    /// Creates the product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST api/products
    /// {
    ///     name: "name product",
    ///     description: "description product",
    ///     price: "price product",
    ///     idProductCategory: "id of product category"
    /// }
    /// </remarks>
    /// <param name="productDto">ProductDto object</param>
    /// <response code="201">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters,
    /// the price is less or equal 0.
    /// </response>
    [HttpPost("addClient")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddClientAsync(ProductDto productDto)
    {
        try
        {
            var productCreateHandler = new ProductCreateHandler(repository, new ProductCreateValidation());
            var product = MapperProductDto.Map(productDto);

            await productCreateHandler.Execute(product);
        }
        catch (Exception)
        {
            throw;
        }

        return Created();
    }


    /// <summary>
    /// Creates the product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST api/products
    /// {
    ///     name: "name product",
    ///     description: "description product",
    ///     price: "price product",
    ///     idProductCategory: "id of product category"
    /// }
    /// </remarks>
    /// <param name="productDto">ProductDto object</param>
    /// <response code="201">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters,
    /// the price is less or equal 0.
    /// </response>
    [HttpPost("addClient1")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddClint1Async(ProductDto productDto)
    {
        try
        {
            var productCreateHandler = new ProductCreateHandler(repository, new ProductCreateValidation());
            var product = MapperProductDto.Map(productDto);

            await productCreateHandler.Execute(product);
        }
        catch (Exception)
        {
            throw;
        }

        return Created();
    }

    /// <summary>
    /// Deletes the product by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /products/1
    /// </remarks>
    /// <param name="id">Id of the product (int)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="404">If product is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Updates the product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT api/products
    /// {
    ///     id: "id of the product to update",
    ///     name: "name product",
    ///     description: "description product",
    ///     price: "price product",
    ///     idProductCategory: "id of product category"
    /// }
    /// </remarks>
    /// <param name="productDto">ProductDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters,
    /// the price is less or equal 0.
    /// </response>
    /// <response code="404">If product is not found by id</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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