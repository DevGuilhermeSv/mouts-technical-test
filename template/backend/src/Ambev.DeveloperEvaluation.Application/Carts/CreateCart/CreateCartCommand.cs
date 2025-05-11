using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Command for creating a new Cart in the system.
/// </summary>
/// <remarks>
/// This command captures the necessary data for creating a Cart, 
/// including the Cart Title, Price, Description, Category, Image, 
/// and Rating. It implements <see cref="IRequest{TResponse}"/> to initiate 
/// the request that returns a <see cref="CreateCartResult"/>. 
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateCartCommandValidator"/>, which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that all fields 
/// are correctly populated and adhere to the required validation rules.
/// </remarks>
public class CreateCartCommand : BaseCartDto, IRequest<CreateCartResult>
{

    public ValidationResultDetail Validate()
    {
        var validator = new CreateCartCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}