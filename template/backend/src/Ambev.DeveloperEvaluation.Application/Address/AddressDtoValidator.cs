using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Address;

public class AddressDtoValidator  : AbstractValidator<BaseAddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must be at most 100 characters.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(150).WithMessage("Street must be at most 150 characters.");

        RuleFor(x => x.Number)
            .GreaterThan(0).WithMessage("Number must be greater than zero.");

        RuleFor(x => x.Zipcode)
            .NotEmpty().WithMessage("Zipcode is required.")
            .Matches(@"^\d{5}-?\d{3}$").WithMessage("Zipcode must be in the format 12345-678 or 12345678.");

        RuleFor(x => x.Geolocation)
            .NotNull().WithMessage("Geolocation is required.");
        
    }
}