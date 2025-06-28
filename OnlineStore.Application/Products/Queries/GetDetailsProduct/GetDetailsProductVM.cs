namespace OnlineStore.Application.Products.Queries.GetDetailsProduct;

public class GetDetailsProductVM
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required GetDetailsProductCategoryVM DetailsProductCategoryVM { get; init; }
}

public class GetDetailsProductCategoryVM
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string? Descrition { get; init; }
}