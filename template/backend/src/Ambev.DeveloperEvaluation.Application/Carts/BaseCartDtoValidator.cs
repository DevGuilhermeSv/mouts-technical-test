namespace Ambev.DeveloperEvaluation.Application.Carts;

using FluentValidation;

public class BaseCartDtoValidator : AbstractValidator<BaseCartDto>
{
    public BaseCartDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Cart ID is required.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.Carts)
            .NotNull().WithMessage("Carts list cannot be null.")
            .Must(p => p.Any()).WithMessage("Cart must contain at least one Cart.");

        RuleForEach(x => x.Carts)
            .SetValidator(new CartCartDtoValidator());
    }
    
    public class CartCartDtoValidator : AbstractValidator<CartCartDto>
    {
        public CartCartDtoValidator()
        {
            RuleFor(x => x.CartId)
                .GreaterThan(0).WithMessage("Cart ID must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");
        }
    }
}