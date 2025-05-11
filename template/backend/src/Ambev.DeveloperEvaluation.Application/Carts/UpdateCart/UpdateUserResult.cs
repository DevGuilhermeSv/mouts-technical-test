using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Represents the response returned after successfully updating a new Cart.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly updated Cart,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateCartResult : BaseCartDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly updated Cart.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated Cart in the system.</value>
    public Guid Id { get; set; }
    
   
}
