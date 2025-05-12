using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSales;

/// <summary>
/// Request model for getting a Sale list
/// </summary>
public class ListSalesRequest
{
    [JsonPropertyName("_page")] public int Page { get; set; } = 1;
    [JsonPropertyName("_size")] public int Size { get; set; } = 10;
    [JsonPropertyName("_order")] public string? Order { get; set; } = null;
}
