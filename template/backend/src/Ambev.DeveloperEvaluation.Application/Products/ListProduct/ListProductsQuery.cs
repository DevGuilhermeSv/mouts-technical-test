using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

public class ListProductsQuery : IRequest<PaginatedList<GetProductResult>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
    
    public string? Title { get;  set; }
    
    public decimal? MinPrice { get;  set; }
    public decimal? MaxPrice { get;  set; }

    public string? Description { get;  set; }
    public string? Category { get;  set; }
}