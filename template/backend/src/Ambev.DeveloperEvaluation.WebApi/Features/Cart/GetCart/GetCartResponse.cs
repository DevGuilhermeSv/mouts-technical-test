using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Carts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart;

/// <summary>
/// API response model for GetCart operation
/// </summary>
public class GetCartResponse : BaseCartDto
{
    /// <summary>
    /// The unique identifier of the Cart
    /// </summary>
    public Guid Id { get; set; }
    
}
