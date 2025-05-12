using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart;

public class ListCartsQuery : IRequest<PaginatedList<GetCartResult>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }

    public DateTime? MinDate { get;  set; }
    
    public DateTime? MaxDate { get;  set; }
}