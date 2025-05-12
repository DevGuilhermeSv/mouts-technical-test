using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersQuery : IRequest<PaginatedList<GetUserResult>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
    
    public string? Username { get; set; } = string.Empty;

   
    public string? Password { get; set; } = string.Empty;

   
    public string? Phone { get; set; } = string.Empty;

    
    public string? Email { get; set; } = string.Empty;

}