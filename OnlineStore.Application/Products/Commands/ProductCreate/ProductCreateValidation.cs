using FluentValidation;
using OnlineStore.Domain;

namespace OnlineStore.Application.Products.Commands.ProductCreate;

public class ProductCreateValidation : AbstractValidator<Product>
{
    private const int MaxNameLength = 2;
    private const int MaxDescriptionLength = 1024;

    public ProductCreateValidation()
    {
        RuleFor(createProductCommand =>
                        createProductCommand.Name)
                            .NotEmpty()
                            .WithMessage("Название товара обязательно для заполнения.")
                            .MaximumLength(MaxNameLength)
                            .WithMessage($"Название товара не должно превышать {MaxNameLength} символов.");

        RuleFor(createProductCommand =>
                        createProductCommand.Description)
                            .NotEmpty()
                            .WithMessage("Описание товара обязательно для заполнения.")
                            .MaximumLength(MaxDescriptionLength)
                            .WithMessage($"Описание товара не должно превышать {MaxDescriptionLength} символов.");

        RuleFor(createProductCommand =>
                        createProductCommand.Price)
                            .GreaterThan(0)
                            .WithMessage("Цена товара должна быть больше нуля.");
    }
}