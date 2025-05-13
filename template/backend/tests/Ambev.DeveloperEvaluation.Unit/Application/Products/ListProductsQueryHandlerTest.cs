using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class ListProductsQueryHandlerTests
{
    private readonly IProductRepository _productRepositoryMock;
    private readonly IMapper _mapperMock;
    private readonly ListProductsQueryHandler _handler;

    public ListProductsQueryHandlerTests()
    {
        _productRepositoryMock = Substitute.For<IProductRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new ListProductsQueryHandler(_productRepositoryMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenCalledWithValidRequest()
    {
        // Arrange
        var request = new ListProductsQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var products = ProductFaker.GenerateMany(5); // Assume Product is a valid entity
        var productsAsQueryable = products.AsQueryable();
        var getProductResult = new GetProductResult();
        var productResultQueryable = products.Select(u => getProductResult).BuildMockDbSet();

        _productRepositoryMock.GetAll(cancellationToken).Returns(productsAsQueryable);
        _mapperMock.ProjectTo<GetProductResult>(Arg.Any<IQueryable<Product>>()).Returns(productResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetProductResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldApplyOrdering_WhenOrderIsProvided()
    {
        // Arrange
        var request = new ListProductsQuery { Page = 1, Size = 10, Order = "productname asc" };
        var cancellationToken = CancellationToken.None;
        var products = ProductFaker.GenerateMany(5); // Assume Product is a valid entity
        var getProductResult = new GetProductResult();
        var productsAsQueryable = products.AsQueryable();
        var productResultQueryable = products.Select(u => getProductResult).AsQueryable().BuildMockDbSet();

        _productRepositoryMock.GetAll("productname", "asc", cancellationToken).Returns(productsAsQueryable);
        _mapperMock.ProjectTo<GetProductResult>(Arg.Any<IQueryable<Product>>()).Returns(productResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetProductResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldDefaultToAsc_WhenInvalidOrderIsProvided()
    {
        // Arrange
        var request = new ListProductsQuery { Page = 1, Size = 10, Order = "productname invalid" };
        var cancellationToken = CancellationToken.None;
        var products = ProductFaker.GenerateMany(5);
        var getProductResult = new GetProductResult();
        var productsAsQueryable = products.AsQueryable();
        var productResultQueryable = products.Select(u => getProductResult).AsQueryable().BuildMockDbSet();

        _productRepositoryMock.GetAll("productname", "asc", cancellationToken).Returns(productsAsQueryable);
        _mapperMock.ProjectTo<GetProductResult>(Arg.Any<IQueryable<Product>>()).Returns(productResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<GetProductResult>>(result);
        Assert.Equal(5, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoProductsFound()
    {
        // Arrange
        var request = new ListProductsQuery { Page = 1, Size = 10, Order = null };
        var cancellationToken = CancellationToken.None;
        var productResultQueryable = new List<GetProductResult>().AsQueryable().BuildMockDbSet();;
        var paginatedList = new PaginatedList<GetProductResult>(new List<GetProductResult>(), 0, 1, 1);

        _productRepositoryMock.GetAll(cancellationToken).Returns(new List<Product>().AsQueryable());
        _mapperMock.ProjectTo<GetProductResult>(Arg.Any<IQueryable<Product>>()).Returns(productResultQueryable);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.Equal(0, result.TotalCount);
    }
}