using Ambev.DeveloperEvaluation.Application.Products;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;

/// <summary>
/// API response model for CreateProduct operation
/// </summary>
public class CreateProductResponse : BaseProductDto
{
    /// <summary>
    /// The unique identifier of the created Product
    /// </summary>
    public Guid Id { get; set; }
    
}
