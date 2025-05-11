using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart;

public class ListCartsResponse : PaginatedList<GetCartResult>
{
    public ListCartsResponse(List<GetCartResult> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
    {
    }
}