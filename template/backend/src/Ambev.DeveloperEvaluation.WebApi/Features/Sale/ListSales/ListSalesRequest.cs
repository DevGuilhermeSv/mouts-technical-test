using System.Text.Json.Serialization;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSales;

/// <summary>
/// Request model for getting a Sale list
/// </summary>
public class ListSalesRequest : BaseFilterRequest
{
    public int? SaleNumber { get;  set; }
    [JsonPropertyName("_minSaleDate")]
    public DateTime? MinSaleDate { get;  set; } 

    [JsonPropertyName("_maxSaleDate")]
    public DateTime? MaxSaleDate { get;  set; } 
    
    public string? Branch { get;  set; }
  
    public bool? IsCancelled { get;  set; }
    
    public decimal? TotalAmount { get;  set; }
}
