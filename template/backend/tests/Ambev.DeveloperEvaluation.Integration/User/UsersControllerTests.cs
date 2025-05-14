using System.Net;
using System.Text;
using Ambev.DeveloperEvaluation.Integration.Fakes;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.User;

public class UsersControllerTests : IClassFixture<CustomWebApplication>
{
    private readonly HttpClient _client;

    public UsersControllerTests(CustomWebApplication factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateUser_ShouldReturn201AndUserData_WhenRequestIsValid()
    {
        // Arrange
        var request = CreateUserRequestTestData.GenerateValidCommand();

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/users", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ApiResponseWithData<CreateUserResponse>>(responseBody);

        responseObject.Should().NotBeNull();
        responseObject!.Success.Should().BeTrue();
        responseObject.Data.Should().NotBeNull();
    }
    [Fact]
    public async Task GetUser_ShouldReturnUser_WhenUserExists()
    {
        // Pré-criação de usuário
        var createRequest = CreateUserRequestTestData.GenerateValidCommand();

        var content = new StringContent(JsonConvert.SerializeObject(createRequest), Encoding.UTF8, "application/json");
        var createResponse = await _client.PostAsync("/api/users", content);
        var createdUser = JsonConvert.DeserializeObject<ApiResponseWithData<CreateUserResponse>>(await createResponse.Content.ReadAsStringAsync());

        var userId = createdUser!.Data.Id;

        // Act
        var response = await _client.GetAsync($"/api/users/{userId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<ApiResponseWithData<GetUserResponse>>(body);

        user.Should().NotBeNull();
        user!.Data.Id.Should().Be(userId);
        
        user!.Data.Email.Should().Be(createRequest.Email);
        
        user!.Data.Phone.Should().Be(createRequest.Phone);
        
        user!.Data.Name.Should().Be(createRequest.Name);
        
        user!.Data.Username.Should().Be(createRequest.Username);
    }
}