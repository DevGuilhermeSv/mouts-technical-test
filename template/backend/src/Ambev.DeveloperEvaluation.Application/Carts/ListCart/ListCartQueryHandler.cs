using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

public class ListCartsQueryHandler : IRequestHandler<ListCartsQuery, PaginatedList<GetCartResult>>
{
    private readonly ICartRepository _CartRepository;
    private readonly IMapper _mapper;

    public ListCartsQueryHandler(ICartRepository CartRepository, IMapper mapper)
    {
        _CartRepository = CartRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetCartResult>> Handle(ListCartsQuery request, CancellationToken cancellationToken)
    {
        var query = _CartRepository.GetAll(cancellationToken);
        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            
            var orderings = request.Order.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderBy = orderings[0];
            string? orderDir = null;
            if(orderings.Length > 1)
                orderDir = (orderings[1] is "desc" or "asc") ? orderings[1] : "desc"; 
            query = _CartRepository.GetAll(orderBy, orderDir, cancellationToken);
        }

        var getCartList = _mapper.ProjectTo<GetCartResult>(query);
        var result = await ListCartsResponse.CreateAsync(getCartList, request.Page, request.Size);

        return result;
    }
}
