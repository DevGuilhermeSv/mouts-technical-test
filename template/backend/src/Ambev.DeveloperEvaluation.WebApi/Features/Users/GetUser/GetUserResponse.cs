using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Users;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class GetUserResponse : BaseUserDto
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }
    
    public BaseAddressDto? Address { get; set; }
    
}
