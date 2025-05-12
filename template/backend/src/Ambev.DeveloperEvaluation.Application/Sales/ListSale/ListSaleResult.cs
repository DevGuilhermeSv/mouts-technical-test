using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

public class ListSalesResponse : PaginatedList<GetSaleResult>
{
    public ListSalesResponse(List<GetSaleResult> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
    {
    }
}