using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Address;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart;

/// <summary>
/// Validator for CreateCartRequest that defines validation rules for Cart creation.
/// </summary>
public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Cartname: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public UpdateCartRequestValidator()
    {
      
    }
}