using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.Fakes;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateCartRequestTestData
{
    public static Faker<CreateCartRequest> CreateCartRequestFaker()
    {
        var cartProductDtoFaker = new Faker<CartProductDto>()
            .CustomInstantiator(f => new CartProductDto
            {
                ProductId = f.Random.Guid(),
                Quantity = f.Random.Int(1, 20)
            });

        return new Faker<CreateCartRequest>()
            .CustomInstantiator(f =>
            {
                var products = cartProductDtoFaker
                    .Generate(f.Random.Int(1, 5));

                return new CreateCartRequest
                {
                    UserId = f.Random.Guid(),
                    Date = f.Date.RecentOffset(7).UtcDateTime,
                    Products = products
                };
            });
    }
    public static Faker<CreateCartRequest> CreateCartRequestFaker(List<Guid> productIdList)
    {

        return new Faker<CreateCartRequest>()
            .CustomInstantiator(f =>
            {
                var products = productIdList.Select(id =>
                {
                    return new Faker<CartProductDto>()
                        .CustomInstantiator(f => new CartProductDto
                        {
                            ProductId = id,
                            Quantity = f.Random.Int(1, 20)
                        }).Generate();
                });
               

                return new CreateCartRequest
                {
                    UserId = f.Random.Guid(),
                    Date = f.Date.RecentOffset(7).UtcDateTime,
                    Products = products.ToList()
                };
            });
    }
    private static Faker<CreateCartRequest> InvalidFaker()
    {
        // Start from a valid template
        var faker = CreateCartRequestFaker().Clone()
            // Invalidate one or more rules:
            .RuleFor(p => p.Products, (List<CartProductDto>)null)
            .RuleFor(p => p.UserId, Guid.Empty)
            .RuleFor(p => p.Date, f => new DateTime());
            
        return faker;
    }

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static CreateCartRequest GenerateValidCommand()
    {
        return CreateCartRequestFaker().Generate();
    }
    public static CreateCartRequest GenerateValidCommand(List<Guid> productIdList)
    {
        return CreateCartRequestFaker(productIdList).Generate();
    }

    public static CreateCartRequest GenerateInValidCommand()
    {
        return InvalidFaker().Generate();
    }
}
