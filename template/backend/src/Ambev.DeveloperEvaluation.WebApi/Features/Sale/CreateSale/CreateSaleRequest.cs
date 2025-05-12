using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Represents a request to create a new Sale in the system.
/// </summary>
public class CreateSaleRequest : BaseSaleDto
{
    /// <summary>
    /// The list of items sold in this sale.
    /// </summary>
    public required  List<SaleItemDto> Items { get;  set; } = new();

}