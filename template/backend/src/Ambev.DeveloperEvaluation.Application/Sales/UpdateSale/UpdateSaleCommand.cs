using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command to update an existing Sale
/// </summary>
public class UpdateSaleCommand : BaseSaleDto, IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the ID of the Sale to update
    /// </summary>
    public Guid Id { get; set; }
    
}