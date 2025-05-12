namespace Ambev.DeveloperEvaluation.Application.Sales;

public class BaseSaleDto
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


    /// <summary>
    /// The list of items sold in this sale.
    /// </summary>
    public required  List<SaleItemDto> Items { get;  set; } = new();
}

public class SaleItemDto
{
    public Guid ProductId { get; private set; }
    /// <summary>
    /// Quantity of the product sold.
    /// </summary>
    public int Quantity { get; private set; }
    
}