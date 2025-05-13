using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Fakes.Application.Sales;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_SaleNotFound_ReturnsNull()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((Sale?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            _saleRepository.DidNotReceive().Update(Arg.Any<Sale>());
            await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_SaleFound_UpdatesSaleAndReturnsResult()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();


            var existingSale = SaleFaker.Generate(); // Substitua pelos valores necessários
            var expectedResult = new UpdateSaleResult
            {
                Id = command.Id,
                SaleDate = default,
                CustomerId = default,
                Items = null
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingSale);

            _mapper.Map(command, existingSale);

            _mapper.Map<UpdateSaleResult>(existingSale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result!.Id);

            _saleRepository.Received(1).Update(existingSale);
            await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
