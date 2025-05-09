using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.ListUsers;

public class ListUsersQueryHandlerTests
{
    private readonly IUserRepository _userRepositoryMock;
    private readonly IMapper _mapperMock;
    private readonly ListUsersQueryHandler _handler;

    public ListUsersQueryHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new ListUsersQueryHandler(_userRepositoryMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenCalledWithValidRequest()
    {
        // Arrange
        var request = new ListUsersQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var users = new List<User> { new User() }; // Assume User is a valid entity
        var usersAsQueryable = users.AsQueryable();
        var userResultQueryable = users.Select(u => new GetUserResult()).BuildMockDbSet();

        _userRepositoryMock.GetAll(cancellationToken).Returns(usersAsQueryable);
        _mapperMock.ProjectTo<GetUserResult>(Arg.Any<IQueryable<User>>()).Returns(userResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetUserResult>>(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldApplyOrdering_WhenOrderIsProvided()
    {
        // Arrange
        var request = new ListUsersQuery { Page = 1, Size = 10, Order = "username asc" };
        var cancellationToken = CancellationToken.None;
        var users = new List<User> { new User() };
        var usersAsQueryable = users.AsQueryable();
        var userResultQueryable = users.Select(u => new GetUserResult()).AsQueryable().BuildMockDbSet();;

        _userRepositoryMock.GetAll("username", "asc", cancellationToken).Returns(usersAsQueryable);
        _mapperMock.ProjectTo<GetUserResult>(Arg.Any<IQueryable<User>>()).Returns(userResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetUserResult>>(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldDefaultToDescending_WhenInvalidOrderIsProvided()
    {
        // Arrange
        var request = new ListUsersQuery { Page = 1, Size = 10, Order = "username invalid" };
        var cancellationToken = CancellationToken.None;
        var users = new List<User> { new User() };
        var usersAsQueryable = users.AsQueryable();
        var userResultQueryable = users.Select(u => new GetUserResult()).AsQueryable().BuildMockDbSet();;

        _userRepositoryMock.GetAll("username", "desc", cancellationToken).Returns(usersAsQueryable);
        _mapperMock.ProjectTo<GetUserResult>(Arg.Any<IQueryable<User>>()).Returns(userResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetUserResult>>(result);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoUsersFound()
    {
        // Arrange
        var request = new ListUsersQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var userResultQueryable = new List<GetUserResult>().AsQueryable().BuildMockDbSet();;
        var paginatedList = new PaginatedList<GetUserResult>(new List<GetUserResult>(), 0, 1, 1);

        _userRepositoryMock.GetAll(cancellationToken).Returns(new List<User>().AsQueryable());
        _mapperMock.ProjectTo<GetUserResult>(Arg.Any<IQueryable<User>>()).Returns(userResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.Equal(0, result.TotalCount);
    }
}