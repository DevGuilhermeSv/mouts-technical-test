using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Represents the response returned after successfully updating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateUserResult : BaseUserDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly updated user.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated user in the system.</value>
    public Guid Id { get; set; }
    
    
    /// <summary>
    /// The address of the user
    /// </summary>
    public BaseAddressDto? Address { get; set; }
   
}
