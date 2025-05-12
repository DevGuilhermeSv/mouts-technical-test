using System.Text.Json.Serialization;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCarts;

/// <summary>
/// Request model for getting a Cart list
/// </summary>
public class ListCartsRequest : BaseFilterRequest
{
    [JsonPropertyName("_minDate")]
    public DateTime? MinDate { get;  set; }
    
    [JsonPropertyName("_maxDate")]
    public DateTime? MaxDate { get;  set; }
}
