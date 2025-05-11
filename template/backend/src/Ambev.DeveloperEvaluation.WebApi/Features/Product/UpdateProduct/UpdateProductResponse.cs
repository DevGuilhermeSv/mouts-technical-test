using Ambev.DeveloperEvaluation.Application.Products;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.UpdateProduct;

/// <summary>
/// API response model for CreateProduct operation
/// </summary>
public class UpdateProductResponse : BaseProductDto
{
    /// <summary>
    /// The unique identifier of the created Product
    /// </summary>
    public Guid Id { get; set; }

}
