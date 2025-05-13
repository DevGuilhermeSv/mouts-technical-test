using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Fakes.Application.Product;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateProductHandler(_productRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ReturnsNull()
        {
            // Arrange
            var command = UpdateProductHandlerTestData.GenerateValidCommand();

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((Product?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            _productRepository.DidNotReceive().Update(Arg.Any<Product>());
            await _productRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ProductFound_UpdatesProductAndReturnsResult()
        {
            // Arrange
            var command = UpdateProductHandlerTestData.GenerateValidCommand();


            var existingProduct = ProductFaker.Generate(); // Substitua pelos valores necessários
            var expectedResult = new UpdateProductResult
            {
                Id = command.Id
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingProduct);

            _mapper.Map(command, existingProduct);

            _mapper.Map<UpdateProductResult>(existingProduct).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result!.Id);

            _productRepository.Received(1).Update(existingProduct);
            await _productRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
