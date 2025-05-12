namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

/// <summary>
/// Request model for getting a Sale by ID
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// The unique identifier of the Sale to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
