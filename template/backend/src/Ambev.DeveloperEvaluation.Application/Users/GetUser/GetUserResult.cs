using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetUserResult : BaseUserDto
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The address of the user
    /// </summary>
    public BaseAddressDto? Address { get; set; }
}
