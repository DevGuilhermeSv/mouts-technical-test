namespace Ambev.DeveloperEvaluation.Application.Products;

using FluentValidation;

public class BaseProductDtoValidator : AbstractValidator<BaseProductDto>
{
    public BaseProductDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(BeAValidUrl).WithMessage("Image must be a valid URL.");

        RuleFor(x => x.Rating)
            .NotNull().WithMessage("Rating is required.")
            .SetValidator(new RatingDtoValidator());
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
public class RatingDtoValidator : AbstractValidator<RatingDto>
{
    public RatingDtoValidator()
    {
        RuleFor(x => x.Rate)
            .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Count cannot be negative.");
    }
}
