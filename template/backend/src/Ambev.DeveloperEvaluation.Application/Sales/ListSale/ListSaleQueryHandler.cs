using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

public class ListSalesQueryHandler : IRequestHandler<ListSalesQuery, PaginatedList<GetSaleResult>>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;

    public ListSalesQueryHandler(ISaleRepository SaleRepository, IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetSaleResult>> Handle(ListSalesQuery request, CancellationToken cancellationToken)
    {
        var query = _SaleRepository.GetAll(cancellationToken);
        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            
            var orderings = request.Order.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderBy = orderings[0];
            string? orderDir = null;
            if(orderings.Length > 1)
                orderDir = (orderings[1] is "desc" or "asc") ? orderings[1] : "desc"; 
            query = _SaleRepository.GetAll(orderBy, orderDir, cancellationToken);
        }

        var getSaleList = _mapper.ProjectTo<GetSaleResult>(query);
        var result = await ListSalesResponse.CreateAsync(getSaleList, request.Page, request.Size);

        return result;
    }
}
