using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult : BaseProductDto
{
    /// <summary>
    /// The unique identifier of the Product
    /// </summary>
    public Guid Id { get; set; }
    
}
