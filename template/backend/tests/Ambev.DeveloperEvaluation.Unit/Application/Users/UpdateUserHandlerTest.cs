using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Fakes.Application.User;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class UpdateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateUserHandler(_userRepository, _mapper);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsNull()
        {
            // Arrange
            var command = UpdateUserHandlerTestData.GenerateValidCommand();

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((User?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            await _userRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            _userRepository.DidNotReceive().Update(Arg.Any<User>());
            await _userRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_UserFound_UpdatesUserAndReturnsResult()
        {
            // Arrange
            var command = UpdateUserHandlerTestData.GenerateValidCommand();


            var existingUser = new User(); // Substitua pelos valores necessários
            var expectedResult = new UpdateUserResult
            {
                Id = command.Id,
                Name = new()
                {
                    FirstName = "John",
                    LastName = "Doe"
                }
            };

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingUser);

            _mapper.Map(command, existingUser);

            _mapper.Map<UpdateUserResult>(existingUser).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result!.Id);

            _userRepository.Received(1).Update(existingUser);
            await _userRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
