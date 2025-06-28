namespace OnlineStore.Domain;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }


    public required int ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }
}