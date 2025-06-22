﻿namespace OnlineStore.Test.Dto;

public class ProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int ProductCategoryId { get; set; }
}