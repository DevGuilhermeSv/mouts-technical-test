using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Users;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateUserRequest : BaseUserDto
{
    public BaseAddressDto? Address {get; set;}
}