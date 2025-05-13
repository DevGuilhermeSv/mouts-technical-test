using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class ListSalesQueryHandlerTests
{
    private readonly ISaleRepository _saleRepositoryMock;
    private readonly IMapper _mapperMock;
    private readonly ListSalesQueryHandler _handler;

    public ListSalesQueryHandlerTests()
    {
        _saleRepositoryMock = Substitute.For<ISaleRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new ListSalesQueryHandler(_saleRepositoryMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenCalledWithValidRequest()
    {
        // Arrange
        var request = new ListSalesQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var sales = SaleFaker.GenerateMany(5); // Assume Sale is a valid entity
        var salesAsQueryable = sales.AsQueryable();
        var getSaleResult = new GetSaleResult
        {
            Items = null,
            SaleDate = default,
            CustomerId = default
        };
        var saleResultQueryable = sales.Select(u => getSaleResult).BuildMockDbSet();

        _saleRepositoryMock.GetAll(cancellationToken).Returns(salesAsQueryable);
        _mapperMock.ProjectTo<GetSaleResult>(Arg.Any<IQueryable<Sale>>()).Returns(saleResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetSaleResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldApplyOrdering_WhenOrderIsProvided()
    {
        // Arrange
        var request = new ListSalesQuery { Page = 1, Size = 10, Order = "salename asc" };
        var cancellationToken = CancellationToken.None;
        var sales = SaleFaker.GenerateMany(5); // Assume Sale is a valid entity
        var getSaleResult = new GetSaleResult
        {
            Items = null,
            SaleDate = default,
            CustomerId = default
        };
        var salesAsQueryable = sales.AsQueryable();
        var saleResultQueryable = sales.Select(u => getSaleResult).AsQueryable().BuildMockDbSet();

        _saleRepositoryMock.GetAll("salename", "asc", cancellationToken).Returns(salesAsQueryable);
        _mapperMock.ProjectTo<GetSaleResult>(Arg.Any<IQueryable<Sale>>()).Returns(saleResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetSaleResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldDefaultToAsc_WhenInvalidOrderIsProvided()
    {
        // Arrange
        var request = new ListSalesQuery { Page = 1, Size = 10, Order = "salename invalid" };
        var cancellationToken = CancellationToken.None;
        var sales = SaleFaker.GenerateMany(5);
        var getSaleResult = new GetSaleResult
        {
            Items = null,
            SaleDate = default,
            CustomerId = default
        };
        var salesAsQueryable = sales.AsQueryable();
        var saleResultQueryable = sales.Select(u => getSaleResult).AsQueryable().BuildMockDbSet();

        _saleRepositoryMock.GetAll("salename", "asc", cancellationToken).Returns(salesAsQueryable);
        _mapperMock.ProjectTo<GetSaleResult>(Arg.Any<IQueryable<Sale>>()).Returns(saleResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetSaleResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoSalesFound()
    {
        // Arrange
        var request = new ListSalesQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var saleResultQueryable = new List<GetSaleResult>().AsQueryable().BuildMockDbSet();;
        var paginatedList = new PaginatedList<GetSaleResult>(new List<GetSaleResult>(), 0, 1, 1);

        _saleRepositoryMock.GetAll(cancellationToken).Returns(new List<Sale>().AsQueryable());
        _mapperMock.ProjectTo<GetSaleResult>(Arg.Any<IQueryable<Sale>>()).Returns(saleResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.Equal(0, result.TotalCount);
    }
}