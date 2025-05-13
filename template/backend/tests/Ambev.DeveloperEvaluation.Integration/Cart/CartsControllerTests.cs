using System.Net;
using System.Text;
using Ambev.DeveloperEvaluation.Integration.Fakes;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Cart;

public class CartsControllerTests : IClassFixture<CustomWebApplication>
{
    private readonly HttpClient _client;

    public CartsControllerTests(CustomWebApplication factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<CreateProductResponse> CreateProduct()
    {
        var productsRequest = CreateProductRequestTestData.GenerateValidCommand();
        var productsContent = new StringContent(JsonConvert.SerializeObject(productsRequest), Encoding.UTF8, "application/json");
        var productResponse = await _client.PostAsync("/api/products", productsContent);
        var responseBody = await productResponse.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ApiResponseWithData<CreateProductResponse>>(responseBody);
        
        return responseObject!.Data;  

    }

    [Fact]
    public async Task CreateCart_ShouldReturn201AndCartData_WhenRequestIsValid()
    {
        
        // Arrange
        var product = await CreateProduct();
        var request = CreateCartRequestTestData.GenerateValidCommand([product.Id]);

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/carts", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ApiResponseWithData<CreateCartResponse>>(responseBody);

        responseObject.Should().NotBeNull();
        responseObject!.Success.Should().BeTrue();
        responseObject.Data.Should().NotBeNull();
    }
    [Fact]
    public async Task CreateCart_ShouldReturn400_WhenRequestIsInvalid()
    {
        // Arrange
        var request = CreateCartRequestTestData.GenerateInValidCommand(); //Create a Cart without previous Created Products 

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/carts", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
    }
    [Fact]
    public async Task GetCart_ShouldReturnCart_WhenCartExists()
    {
        //Arrange
        var createdProduct = await CreateProduct();
        var createRequest = CreateCartRequestTestData.GenerateValidCommand([createdProduct.Id]);

        var content = new StringContent(JsonConvert.SerializeObject(createRequest), Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/api/carts", content);
        var createdCart = JsonConvert.DeserializeObject<ApiResponseWithData<CreateCartResponse>>(await createResponse.Content.ReadAsStringAsync());

        var cartId = createdCart!.Data.Id;

        // Act
        var response = await _client.GetAsync($"/api/carts/{cartId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStringAsync();
        var cart = JsonConvert.DeserializeObject<ApiResponseWithData<GetCartResponse>>(body);

        cart.Should().NotBeNull();
        cart!.Data.Id.Should().Be(cartId);
        
        cart!.Data.UserId.Should().Be(createRequest.UserId);
        
        cart!.Data.Date.Should().Be(createRequest.Date);
        
    }
}