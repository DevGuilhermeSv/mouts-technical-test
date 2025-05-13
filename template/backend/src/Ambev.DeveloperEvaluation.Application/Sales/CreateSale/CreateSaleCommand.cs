using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new Sale in the system.
/// </summary>
/// <remarks>
/// This command captures the necessary data for creating a Sale, 
/// including the Sale Title, Price, Description, Category, Image, 
/// and Rating. It implements <see cref="IRequest{TResponse}"/> to initiate 
/// the request that returns a <see cref="CreateSaleResult"/>. 
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleValidator"/>, which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that all fields 
/// are correctly populated and adhere to the required validation rules.
/// </remarks>
public class CreateSaleCommand : BaseSaleDto, IRequest<CreateSaleResult>
{
    /// <summary>
    /// The list of items sold in this sale.
    /// </summary>
    public required  List<SaleItemDto> Items { get;  set; } = new();

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}