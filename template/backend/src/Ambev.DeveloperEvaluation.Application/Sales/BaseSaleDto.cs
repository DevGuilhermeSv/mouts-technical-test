namespace Ambev.DeveloperEvaluation.Application.Sales;

public abstract class BaseSaleDto
{
    /// <summary>
    /// The date and time the sale was made.
    /// </summary>
    public required  DateTime SaleDate { get;  set; }

    /// <summary>
    /// The name or identifier of the customer.
    /// </summary>
    public required Guid CustomerId { get;  set; }

    /// <summary>
    /// The branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;
    
}

public class SaleItemDto
{
    public Guid ProductId { get; set; }
    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
    
}