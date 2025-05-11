using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Products;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.GetProduct;

/// <summary>
/// API response model for GetProduct operation
/// </summary>
public class GetProductResponse : BaseProductDto
{
    /// <summary>
    /// The unique identifier of the Product
    /// </summary>
    public Guid Id { get; set; }
    
}
