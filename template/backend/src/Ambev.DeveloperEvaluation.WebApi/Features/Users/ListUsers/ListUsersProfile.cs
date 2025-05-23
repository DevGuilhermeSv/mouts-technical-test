using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Profile for mapping ListUser feature requests to commands
/// </summary>
public class ListUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListUser feature
    /// </summary>
    public ListUsersProfile()
    {
        CreateMap<ListUsersRequest, ListUsersQuery>();
    }
}
