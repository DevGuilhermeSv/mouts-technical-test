using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command to update an existing user
/// </summary>
public class UpdateUserCommand : BaseUserDto, IRequest<UpdateUserResult>
{
    /// <summary>
    /// Gets or sets the ID of the user to update
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The address of the user
    /// </summary>
    public BaseAddressDto? Address { get; set; }
    
}