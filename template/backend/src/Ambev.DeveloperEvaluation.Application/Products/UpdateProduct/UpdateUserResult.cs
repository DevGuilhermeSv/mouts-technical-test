using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully updating a new Product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly updated Product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateProductResult : BaseProductDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly updated Product.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated Product in the system.</value>
    public Guid Id { get; set; }
    
   
}
