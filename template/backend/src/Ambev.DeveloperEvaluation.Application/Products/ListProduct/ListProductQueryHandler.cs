using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, PaginatedList<GetProductResult>>
{
    private readonly IProductRepository _ProductRepository;
    private readonly IMapper _mapper;

    public ListProductsQueryHandler(IProductRepository ProductRepository, IMapper mapper)
    {
        _ProductRepository = ProductRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetProductResult>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _ProductRepository.GetAll(cancellationToken);
        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            
            var orderings = request.Order.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderBy = orderings[0];
            string? orderDir = null;
            if(orderings.Length > 1)
                orderDir = (orderings[1] is "desc" or "asc") ? orderings[1] : "desc"; 
            query = _ProductRepository.GetAll(orderBy, orderDir, cancellationToken);
        }

        var getProductList = _mapper.ProjectTo<GetProductResult>(query);
        var result = await ListProductsResponse.CreateAsync(getProductList, request.Page, request.Size);

        return result;
    }
}
