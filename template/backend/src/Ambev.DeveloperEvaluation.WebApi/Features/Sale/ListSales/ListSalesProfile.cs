using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSales;

/// <summary>
/// Profile for mapping ListSale feature requests to commands
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSale feature
    /// </summary>
    public ListSalesProfile()
    {
        CreateMap<ListSalesRequest, ListSalesQuery>();
    }
}
