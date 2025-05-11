using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

public class ListProductsResponse : PaginatedList<GetProductResult>
{
    public ListProductsResponse(List<GetProductResult> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
    {
    }
}