using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Response model for GetCart operation
/// </summary>
public class GetCartResult : BaseCartDto
{
    /// <summary>
    /// The unique identifier of the Cart
    /// </summary>
    public Guid Id { get; set; }
    
}
