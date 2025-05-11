using Ambev.DeveloperEvaluation.Application.Users;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class UpdateUserResponse : BaseUserDto
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

}
