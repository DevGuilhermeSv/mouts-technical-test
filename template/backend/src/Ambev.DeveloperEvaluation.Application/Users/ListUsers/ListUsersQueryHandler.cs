using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PaginatedList<GetUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ListUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetUserResult>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.GetAll(cancellationToken);
        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            
            var orderings = request.Order.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderBy = orderings[0];
            string? orderDir = null;
            if(orderings.Length > 1)
                orderDir = (orderings[1] is "desc" or "asc") ? orderings[1] : "desc"; 
            query = _userRepository.GetAll(orderBy, orderDir, cancellationToken);
        }

        var getUserList = _mapper.ProjectTo<GetUserResult>(query);
        var result = await ListUsersResponse.CreateAsync(getUserList, request.Page, request.Size);

        return result;
    }
}
