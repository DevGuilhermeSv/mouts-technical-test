using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult?>
{
    private readonly IProductRepository _ProductRepository;
    private readonly IMapper _mapper;
    /// <summary>
    /// Initializes a new instance of UpdateProductHandler
    /// </summary>
    /// <param name="ProductRepository">The Product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateProductCommand</param>
    public UpdateProductHandler(IProductRepository ProductRepository, IMapper mapper)
    {
        _ProductRepository = ProductRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request
    /// </summary>
    /// <param name="command">The UpdateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product details</returns>
    public async Task<UpdateProductResult?> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var Product = await _ProductRepository.GetByIdAsync(command.Id, cancellationToken);
        if (Product == null)
        {
            return null;
        }

        _mapper.Map(command, Product); 

        _ProductRepository.Update(Product);
        await _ProductRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateProductResult>(Product);
    }
}
