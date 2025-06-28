namespace OnlineStore.Application.Products.Queries.GetAllProducts;

public class GetAllProductsVM
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required string NameProductCategory { get; init; }
}