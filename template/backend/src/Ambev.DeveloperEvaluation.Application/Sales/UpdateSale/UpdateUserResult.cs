namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the response returned after successfully updating a new Sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly updated Sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateSaleResult : BaseSaleDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly updated Sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated Sale in the system.</value>
    public Guid Id { get; set; }
    
   
}
