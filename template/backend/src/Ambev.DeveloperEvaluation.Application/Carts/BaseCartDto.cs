namespace Ambev.DeveloperEvaluation.Application.Carts;

/// <summary>
/// Data Transfer Object representing a shopping cart.
/// </summary>
public class BaseCartDto
{
    /// <summary>
    /// The ID of the user associated with this cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The date the cart was created or last modified.
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// List of Carts in the cart.
    /// </summary>
    public List<CartProductDto> Products { get; set; } = new();
}

/// <summary>
/// DTO representing a Cart and its quantity inside a cart.
/// </summary>
public class CartProductDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}