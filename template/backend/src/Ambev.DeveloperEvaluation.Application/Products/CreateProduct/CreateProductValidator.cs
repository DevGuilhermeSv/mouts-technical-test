using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Validator for CreateProductCommand
    /// </summary>
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(100).WithMessage("Category must be at most 100 characters.");

            RuleFor(x => x.Image)
                .MaximumLength(200).WithMessage("Image URL must be at most 200 characters.")
                .Matches(@"^(http|https):\/\/").When(x => !string.IsNullOrWhiteSpace(x.Image))
                .WithMessage("Image must be a valid URL.");

            RuleFor(x => x.Rating).NotNull().WithMessage("Rating is required.");

            When(x => x.Rating != null, () =>
            {
                RuleFor(x => x.Rating.Rate)
                    .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");

                RuleFor(x => x.Rating.Count)
                    .GreaterThanOrEqualTo(0).WithMessage("Rating count must be zero or greater.");
            });
        }
    }
}