using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    /// <summary>
    /// Validator for the CreateCartCommand.
    /// </summary>
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId must not be empty.");

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Date cannot be in the future.");

            RuleFor(x => x.Products)
                .NotNull()
                .WithMessage("Products must not be null.")
                .NotEmpty()
                .WithMessage("At least one product must be provided.");

            RuleForEach(x => x.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.ProductId)
                    .NotEmpty()
                    .WithMessage("ProductId must not be empty.");

                product.RuleFor(p => p.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0.");
            });
        }
    }
}