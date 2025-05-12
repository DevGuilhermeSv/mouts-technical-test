using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an individual item in a sale, including price, quantity and discount.
/// </summary>
public class SaleItem : BaseEntity
{
    public Guid SaleId { get; private set; } = Guid.Empty;
    public Sale? Sale { get; private set; }
    
    /// <summary>
    /// Product name or identifier.
    /// </summary>
    public Guid ProductId { get; private set; }
    public Product? Product { get; private set; }

    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Discount applied to this item.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// Total price for this item, considering quantity and discount.
    /// </summary>
    public decimal Total => (UnitPrice * Quantity) - Discount;

    protected SaleItem() { }

    public SaleItem(Guid productId, int quantity, decimal unitPrice, decimal discount)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");
        if (unitPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");
        if (discount < 0)
            throw new ArgumentOutOfRangeException(nameof(discount), "Discount cannot be negative.");

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }
}