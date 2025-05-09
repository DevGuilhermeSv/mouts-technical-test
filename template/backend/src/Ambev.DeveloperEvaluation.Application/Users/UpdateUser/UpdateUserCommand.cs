using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command to update an existing user
/// </summary>
public class UpdateUserCommand : IRequest<UpdateUserResult>
{
    /// <summary>
    /// Gets or sets the ID of the user to update
    /// </summary>
    public Guid Id { get; set; }
    
}