using System.Text.Json.Serialization;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.ListProducts;

/// <summary>
/// Request model for getting a Product list
/// </summary>
public class ListProductsRequest : BaseFilterRequest
{
    public string? Title { get;  set; }
    [JsonPropertyName("_minPrice")]
    public decimal? MinPrice { get;  set; }
    [JsonPropertyName("_maxPrice")]
    public decimal? MaxPrice { get;  set; }
    public string? Description { get;  set; }
    public string? Category { get;  set; }
}
