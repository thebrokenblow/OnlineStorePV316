namespace OnlineStore.VM;

public class ProductDetailsVM
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required ProductDetailsCategoryVM ProductDetailsCategoryVM { get; init; }
}

public class ProductDetailsCategoryVM
{
    public required int IdProductCategory { get; init; }
    public required string? NameProductCategory { get; init; }
    public required string? DescriptionProductCategory { get; init; }
}