using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Fakes.Application.Cart;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class UpdateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly UpdateCartHandler _handler;

        public UpdateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateCartHandler(_cartRepository, _mapper);
        }

        [Fact]
        public async Task Handle_CartNotFound_ReturnsNull()
        {
            // Arrange
            var command = UpdateCartHandlerTestData.GenerateValidCommand();

            _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((Cart?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            await _cartRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            _cartRepository.DidNotReceive().Update(Arg.Any<Cart>());
            await _cartRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_CartFound_UpdatesCartAndReturnsResult()
        {
            // Arrange
            var command = UpdateCartHandlerTestData.GenerateValidCommand();


            var existingCart = CartFaker.Generate(); // Substitua pelos valores necessários
            var expectedResult = new UpdateCartResult
            {
                Id = command.Id
            };

            _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingCart);

            _mapper.Map(command, existingCart);

            _mapper.Map<UpdateCartResult>(existingCart).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result!.Id);

            _cartRepository.Received(1).Update(existingCart);
            await _cartRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
