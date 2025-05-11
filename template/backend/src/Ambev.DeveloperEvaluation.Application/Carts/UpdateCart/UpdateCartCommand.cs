using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command to update an existing Cart
/// </summary>
public class UpdateCartCommand : BaseCartDto, IRequest<UpdateCartResult>
{
    /// <summary>
    /// Gets or sets the ID of the Cart to update
    /// </summary>
    public Guid Id { get; set; }
    
}