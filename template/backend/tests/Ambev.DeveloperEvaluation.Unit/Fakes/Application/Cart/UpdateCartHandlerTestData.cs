using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Fakes.Application.Cart;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateCartHandlerTestData
{
    /// <summary>
    /// Returns a configured Faker for generating UpdateCartCommand objects.
    /// </summary>
    public static Faker<UpdateCartCommand> UpdateCartHandlerFaker()
    {
        var cartProductDtoFaker = new Faker<CartProductDto>()
            .CustomInstantiator(f => new CartProductDto
            {
                ProductId = f.Random.Guid(),
                Quantity = f.Random.Int(1, 20)
            });

        return new Faker<UpdateCartCommand>()
            .CustomInstantiator(f =>
            {
                var products = cartProductDtoFaker
                    .Generate(f.Random.Int(1, 5));

                return new UpdateCartCommand
                {
                    Id = f.Random.Guid(),
                    UserId = f.Random.Guid(),
                    Date = f.Date.RecentOffset(7).UtcDateTime,
                    Products = products
                };
            });
    }



    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static UpdateCartCommand GenerateValidCommand()
    {
        return UpdateCartHandlerFaker().Generate();
    }
    /// <summary>
    /// Generates a valid List of Cart entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid List of Cart entity with randomly generated data.</returns>
    public static List<UpdateCartCommand> GenerateValidCommand(int count)
    {
        return UpdateCartHandlerFaker().Generate(count);
    }
}
