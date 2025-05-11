using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCarts;

/// <summary>
/// Profile for mapping ListCart feature requests to commands
/// </summary>
public class ListCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCart feature
    /// </summary>
    public ListCartsProfile()
    {
        CreateMap<ListCartsRequest, ListCartsQuery>();
    }
}
