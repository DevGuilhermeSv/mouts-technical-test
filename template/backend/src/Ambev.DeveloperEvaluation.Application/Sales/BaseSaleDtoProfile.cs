using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class BaseSaleDtoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public BaseSaleDtoProfile()
    {
        CreateMap<BaseSaleDto, Sale>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CustomerId));
        
        CreateMap<Sale, BaseSaleDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId));
        
        CreateMap<SaleItemDto, SaleItem>().ReverseMap();
    }
}
