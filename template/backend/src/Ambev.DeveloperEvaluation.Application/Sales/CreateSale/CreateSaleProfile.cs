using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ConstructUsing(cmd => new Sale(cmd.SaleDate, cmd.CustomerId, cmd.Branch,
                cmd.Items.Select(item => new SaleItem(item.ProductId, item.Quantity, item.UnitPrice)).ToList()));
        
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId));
        
        CreateMap<SaleItemDto, SaleItem>().ReverseMap();
    }
}
