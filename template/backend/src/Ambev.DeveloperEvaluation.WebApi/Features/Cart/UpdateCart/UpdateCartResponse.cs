using Ambev.DeveloperEvaluation.Application.Carts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart;

/// <summary>
/// API response model for CreateCart operation
/// </summary>
public class UpdateCartResponse : BaseCartDto
{
    /// <summary>
    /// The unique identifier of the created Cart
    /// </summary>
    public Guid Id { get; set; }

}
