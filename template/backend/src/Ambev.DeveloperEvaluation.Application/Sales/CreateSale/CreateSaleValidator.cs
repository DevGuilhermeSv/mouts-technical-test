using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.SaleDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("SaleDate cannot be in the future.");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(100).WithMessage("Branch must be at most 100 characters.");

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Items list cannot be null.")
                .NotEmpty().WithMessage("At least one sale item is required.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("ProductId is required.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");
            });
        }
    }
}