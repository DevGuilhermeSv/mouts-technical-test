namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;

/// <summary>
/// Request model for deleting a Sale
/// </summary>
public class DeleteSaleRequest
{
    /// <summary>
    /// The unique identifier of the Sale to delete
    /// </summary>
    public Guid Id { get; set; }
}
