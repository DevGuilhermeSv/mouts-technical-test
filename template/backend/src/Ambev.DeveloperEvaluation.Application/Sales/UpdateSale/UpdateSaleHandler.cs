using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult?>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;
    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateSaleCommand</param>
    public UpdateSaleHandler(ISaleRepository SaleRepository, IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale details</returns>
    public async Task<UpdateSaleResult?> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var Sale = await _SaleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (Sale == null)
        {
            return null;
        }

        _mapper.Map(command, Sale); 

        _SaleRepository.Update(Sale);
        await _SaleRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateSaleResult>(Sale);
    }
}
