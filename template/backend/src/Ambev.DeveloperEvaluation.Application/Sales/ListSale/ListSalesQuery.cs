using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

public class ListSalesQuery : IRequest<PaginatedList<GetSaleResult>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
    
    public DateTime? MinSaleDate { get;  set; } 
   
    public DateTime? MaxSaleDate { get;  set; } 
    
    public string? Branch { get;  set; }
  
    public bool? IsCancelled { get;  set; }
    
    public decimal? TotalAmount { get;  set; }
}