using System.Net;
using System.Text;
using Ambev.DeveloperEvaluation.Integration.Fakes;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.GetProduct;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Product;

public class ProductsControllerTests : IClassFixture<CustomWebApplication>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplication factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ShouldReturn201AndProductData_WhenRequestIsValid()
    {
        
        // Arrange
        var request = CreateProductRequestTestData.GenerateValidCommand();

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ApiResponseWithData<CreateProductResponse>>(responseBody);

        responseObject.Should().NotBeNull();
        responseObject!.Success.Should().BeTrue();
        responseObject.Data.Should().NotBeNull();
    }
    [Fact]
    public async Task CreateProduct_ShouldReturn400_WhenRequestIsInvalid()
    {
        // Arrange
        //Create a Product without previous Created Products 
        var request = CreateProductRequestTestData.GenerateInValidCommand(); 

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ApiResponseWithData<CreateProductResponse>>(responseBody);

        responseObject.Should().NotBeNull();
        responseObject!.Success.Should().BeFalse();
        responseObject.Data.Should().BeNull();
    }
    [Fact]
    public async Task GetProduct_ShouldReturnProduct_WhenProductExists()
    {
        var createRequest = CreateProductRequestTestData.GenerateValidCommand();

        var content = new StringContent(JsonConvert.SerializeObject(createRequest), Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/api/products", content);
        var createdProduct = JsonConvert.DeserializeObject<ApiResponseWithData<CreateProductResponse>>(await createResponse.Content.ReadAsStringAsync());

        var productId = createdProduct!.Data.Id;

        // Act
        var response = await _client.GetAsync($"/api/products/{productId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ApiResponseWithData<GetProductResponse>>(body);

        product.Should().NotBeNull();
        product!.Data.Id.Should().Be(productId);
        
        product!.Data.Category.Should().Be(createRequest.Category);
        
        product!.Data.Image.Should().Be(createRequest.Image);
        
        product!.Data.Price.Should().Be(createRequest.Price);
    }
}