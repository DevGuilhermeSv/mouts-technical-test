using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product in the system.
/// </summary>
/// <remarks>
/// This command captures the necessary data for creating a product, 
/// including the product Title, Price, Description, Category, Image, 
/// and Rating. It implements <see cref="IRequest{TResponse}"/> to initiate 
/// the request that returns a <see cref="CreateProductResult"/>. 
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductValidator"/>, which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that all fields 
/// are correctly populated and adhere to the required validation rules.
/// </remarks>
public class CreateProductCommand : BaseProductDto, IRequest<CreateProductResult>
{

    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}