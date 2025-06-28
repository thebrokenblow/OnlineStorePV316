using FluentValidation;
using OnlineStore.Application.Dto;

namespace OnlineStore.Application.Products.Queries.GetRangeProducts;

public class GetRangeProductsValidation : AbstractValidator<RangeItemsDto>
{
    private const int MaxCountTake = 1000;

    public GetRangeProductsValidation()
    {
        RuleFor(getProductRangeQuery =>
                    getProductRangeQuery.CountSkip)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("Количество пропускаемых элементов не может быть отрицательным.");

        RuleFor(getProductRangeQuery =>
                    getProductRangeQuery.CountTake)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("Количество запрашиваемых элементов не может быть отрицательным.")
                        .LessThanOrEqualTo(MaxCountTake)
                        .WithMessage($"Количество запрашиваемых элементов не может превышать {MaxCountTake}.");
    }
}