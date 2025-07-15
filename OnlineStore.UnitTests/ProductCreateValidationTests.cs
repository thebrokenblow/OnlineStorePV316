using FluentValidation.Results;
using OnlineStore.Application.Products.Commands.ProductCreate;
using OnlineStore.Domain;
using Xunit;

namespace OnlineStore.UnitTests;

public class ProductCreateValidationTests
{
    private readonly ProductCreateValidation _validator = new ProductCreateValidation();

    private Product CreateValidProduct() => new Product
    {
        Name = "Valid Name",
        Description = "Valid Description",
        Price = 10.99m,
        ProductCategoryId = 1
    };

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Name = string.Empty;

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "Name" &&
            e.ErrorMessage == "Название продукта не должно быть пустым.");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Exceeds_MaxLength()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Name = new string('a', ProductCreateValidation.MaxNameLength + 1);

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "Name" &&
            e.ErrorMessage == $"Название продукта не должно превышать {ProductCreateValidation.MaxNameLength} символов.");
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Description = string.Empty;

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "Description" &&
            e.ErrorMessage == "Описание продукта не должно быть пустым.");
    }

    [Fact]
    public void Should_Have_Error_When_Description_Exceeds_MaxLength()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Description = new string('a', ProductCreateValidation.MaxDescriptionLength + 1);

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "Description" &&
            e.ErrorMessage == $"Описание продукта не должно превышать {ProductCreateValidation.MaxDescriptionLength} символов.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Should_Have_Error_When_Price_Is_Not_Positive(decimal price)
    {
        // Arrange
        var product = CreateValidProduct();
        product.Price = price;

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "Price" &&
            e.ErrorMessage == "Цена товара не должна быть отрицательной или равна нулю");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Should_Have_Error_When_ProductCategoryId_Is_Not_Positive(int categoryId)
    {
        // Arrange
        var product = CreateValidProduct();
        product.ProductCategoryId = categoryId;

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e =>
            e.PropertyName == "ProductCategoryId" &&
            e.ErrorMessage == "Идентификатор категории должен быть положительным числом");
    }

    [Fact]
    public void Should_Pass_When_All_Fields_Are_Valid()
    {
        // Arrange
        var product = CreateValidProduct();

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(0.01)]
    public void Should_Pass_When_Price_Is_Positive(decimal price)
    {
        // Arrange
        var product = CreateValidProduct();
        product.Price = price;

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void Should_Pass_When_ProductCategoryId_Is_Positive(int categoryId)
    {
        // Arrange
        var product = CreateValidProduct();
        product.ProductCategoryId = categoryId;

        // Act
        ValidationResult result = _validator.Validate(product);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}