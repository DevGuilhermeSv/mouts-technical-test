namespace Ambev.DeveloperEvaluation.Application.Carts;

using FluentValidation;

public class BaseCartDtoValidator : AbstractValidator<BaseCartDto>
{
    public BaseCartDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.Products)
            .NotNull().WithMessage("Carts list cannot be null.")
            .Must(p => p.Any()).WithMessage("Cart must contain at least one Cart.");

        RuleForEach(x => x.Products)
            .SetValidator(new CartProductDtoValidator());
    }
    
    public class CartProductDtoValidator : AbstractValidator<CartProductDto>
    {
        public CartProductDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().
                NotEmpty().
                WithMessage("Product ID cant be null.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");
        }
    }
}