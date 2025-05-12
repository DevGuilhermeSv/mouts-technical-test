namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult : BaseSaleDto
{
    /// <summary>
    /// The unique identifier of the Sale
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    ///
    public int SaleNumber { get; set; }
    
    /// <summary>
    /// Indicates whether the sale was cancelled.
    /// </summary>
    public bool IsCancelled { get;  set; }

    /// <summary>
    /// The total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// The list of items sold in this sale.
    /// </summary>
    public required  List<SaleItemResult> Items { get;  set; } = new();
    
}

public class SaleItemResult : SaleItemDto
{
    
    /// <summary>
    /// Discount applied to this item.
    /// </summary>
    public decimal Discount { get; init; }
    
    /// <summary>
    /// Total price for this item, considering quantity and discount.
    /// </summary>
    public decimal Total { get; init; }
    
}
