namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart;

/// <summary>
/// Request model for getting a Cart by ID
/// </summary>
public class GetCartRequest
{
    /// <summary>
    /// The unique identifier of the Cart to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
