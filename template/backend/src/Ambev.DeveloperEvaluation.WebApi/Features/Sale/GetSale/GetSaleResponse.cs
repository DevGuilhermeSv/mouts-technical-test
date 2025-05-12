using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResponse : BaseSaleDto
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
