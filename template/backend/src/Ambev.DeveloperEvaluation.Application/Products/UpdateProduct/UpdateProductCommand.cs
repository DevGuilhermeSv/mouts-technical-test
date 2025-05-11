using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Command to update an existing Product
/// </summary>
public class UpdateProductCommand : BaseProductDto, IRequest<UpdateProductResult>
{
    /// <summary>
    /// Gets or sets the ID of the Product to update
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The address of the Product
    /// </summary>
    public BaseAddressDto? Address { get; set; }
    
}