using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, PaginatedList<GetProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetProductResult>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.GetAll(cancellationToken);
        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            
            var orderings = request.Order.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderBy = orderings[0];
            string? orderDir = null;
            if(orderings.Length > 1)
                orderDir = (orderings[1] is "desc" or "asc") ? orderings[1] : "desc"; 
            query = _productRepository.GetAll(orderBy, orderDir, cancellationToken);
        }

        query = _productRepository.Filter(query, "Title", request.Title);
        
        query = _productRepository.Filter(query, "Description", request.Description);
        
        query = _productRepository.Filter(query, "Category", request.Category);
        
        query = _productRepository.NumericFilter(query, "_minPrice", request.MinPrice);
        
        query = _productRepository.NumericFilter(query, "_maxPrice", request.MaxPrice);


        var getProductList = _mapper.ProjectTo<GetProductResult>(query);
        var result = await ListProductsResponse.CreateAsync(getProductList, request.Page, request.Size);

        return result;
    }
}
