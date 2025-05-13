using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class ListCartsQueryHandlerTests
{
    private readonly ICartRepository _cartRepositoryMock;
    private readonly IMapper _mapperMock;
    private readonly ListCartsQueryHandler _handler;

    public ListCartsQueryHandlerTests()
    {
        _cartRepositoryMock = Substitute.For<ICartRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new ListCartsQueryHandler(_cartRepositoryMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenCalledWithValidRequest()
    {
        // Arrange
        var request = new ListCartsQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var carts = CartFaker.GenerateMany(5); // Assume Cart is a valid entity
        var cartsAsQueryable = carts.AsQueryable();
        var getCartResult = new GetCartResult();
        var cartResultQueryable = carts.Select(u => getCartResult).BuildMockDbSet();

        _cartRepositoryMock.GetAll(cancellationToken).Returns(cartsAsQueryable);
        _mapperMock.ProjectTo<GetCartResult>(Arg.Any<IQueryable<Cart>>()).Returns(cartResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetCartResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldApplyOrdering_WhenOrderIsProvided()
    {
        // Arrange
        var request = new ListCartsQuery { Page = 1, Size = 10, Order = "cartname asc" };
        var cancellationToken = CancellationToken.None;
        var carts = CartFaker.GenerateMany(5); // Assume Cart is a valid entity
        var getCartResult = new GetCartResult();
        var cartsAsQueryable = carts.AsQueryable();
        var cartResultQueryable = carts.Select(u => getCartResult).AsQueryable().BuildMockDbSet();

        _cartRepositoryMock.GetAll("cartname", "asc", cancellationToken).Returns(cartsAsQueryable);
        _mapperMock.ProjectTo<GetCartResult>(Arg.Any<IQueryable<Cart>>()).Returns(cartResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetCartResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldDefaultToAsc_WhenInvalidOrderIsProvided()
    {
        // Arrange
        var request = new ListCartsQuery { Page = 1, Size = 10, Order = "cartname invalid" };
        var cancellationToken = CancellationToken.None;
        var carts = CartFaker.GenerateMany(5);
        var getCartResult = new GetCartResult();
        var cartsAsQueryable = carts.AsQueryable();
        var cartResultQueryable = carts.Select(u => getCartResult).AsQueryable().BuildMockDbSet();

        _cartRepositoryMock.GetAll("cartname", "asc", cancellationToken).Returns(cartsAsQueryable);
        _mapperMock.ProjectTo<GetCartResult>(Arg.Any<IQueryable<Cart>>()).Returns(cartResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetCartResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoCartsFound()
    {
        // Arrange
        var request = new ListCartsQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var cartResultQueryable = new List<GetCartResult>().AsQueryable().BuildMockDbSet();;
        var paginatedList = new PaginatedList<GetCartResult>(new List<GetCartResult>(), 0, 1, 1);

        _cartRepositoryMock.GetAll(cancellationToken).Returns(new List<Cart>().AsQueryable());
        _mapperMock.ProjectTo<GetCartResult>(Arg.Any<IQueryable<Cart>>()).Returns(cartResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.Equal(0, result.TotalCount);
    }
}