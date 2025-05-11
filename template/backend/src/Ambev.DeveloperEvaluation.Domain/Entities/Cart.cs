using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart associated with a user and a list of products.
/// </summary>
public class Cart: BaseEntity
{
    /// <summary>
    /// The ID of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; private set; }
    public User? User {get; private set;}

    /// <summary>
    /// The date the cart was created or updated.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// Collection of products in the cart with their respective quantities.
    /// </summary>
    public List<CartProduct> Products { get; private set; } = new();

    protected Cart() { } 

    public Cart(Guid userId, DateTime date, List<CartProduct> products)
    {
        UserId = userId;
        Date = date;
        Products = products ?? throw new ArgumentNullException(nameof(products));
    }
}

/// <summary>
/// Represents a product and its quantity in a cart.
/// </summary>
public class CartProduct :BaseEntity
{
    public Guid CartId { get; private set; }
    public Cart Cart { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    protected CartProduct() { }

    public CartProduct(Guid productId, int quantity, Guid? cartId)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be at least 1.");

        ProductId = productId;
        Quantity = quantity;
        CartId = cartId ?? Guid.Empty;
    }
}
