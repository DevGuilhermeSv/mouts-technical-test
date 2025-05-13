using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.Fakes;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductRequestTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated users will have valid:
    /// - Title
    /// - Price
    /// - Description
    /// - Category
    /// - Image
    /// </summary>
    private static Faker<CreateProductRequest> CreateProductHandlerFaker()
    {
        var ratingFaker = new Faker<RatingDto>()
        .RuleFor(r => r.Rate, f => Math.Round(f.Random.Double(1.0, 5.0), 1))
        .RuleFor(r => r.Count, f => f.Random.Int(1, 1000));

        return new Faker<CreateProductRequest>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(5, 500)))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Rating, f => ratingFaker.Generate());
    }


    /// <summary>
    /// Base Faker configured to generate invalid CreateProductRequest objects.
    /// Each call will set at least one field to an invalid value.
    /// </summary>
    private static Faker<CreateProductRequest> InvalidFaker()
    {
        // Start from a valid template
        var faker = CreateProductHandlerFaker().Clone()
            // Invalidate one or more rules:
            .RuleFor(p => p.Title,      f => string.Empty)
            .RuleFor(p => p.Price,      f => -1m)
            .RuleFor(p => p.Description,f => string.Empty)
            .RuleFor(p => p.Category,   f => string.Empty)
            .RuleFor(p => p.Image,      f => "not-a-url")
            .RuleFor(p => p.Rating,     f => new RatingDto { Rate = 0, Count = -10 });

        return faker;
    }


    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static CreateProductRequest GenerateValidCommand()
    {
        return CreateProductHandlerFaker().Generate();
    }
    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static List<CreateProductRequest> GenerateValidCommand(int count)
    {
        return CreateProductHandlerFaker().Generate(count);
    }

    public static CreateProductRequest GenerateInValidCommand()
    {
        return InvalidFaker().Generate();
    }
}
