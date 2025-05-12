using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale operation
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>().IncludeBase<BaseSaleDto,Sale>()
            .ConstructUsing(cmd => new Sale(cmd.SaleDate, cmd.CustomerId, cmd.Branch,
                cmd.Items.Select(item => new SaleItem(item.ProductId, item.Quantity, item.UnitPrice)).ToList()));
        
        CreateMap<Sale, UpdateSaleResult>().IncludeBase<Sale,BaseSaleDto>();
    }
}
