using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Fakes.Application.Product;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateProductHandlerTestData
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
    private static Faker<UpdateProductCommand> UpdateProductHandlerFaker()
    {
        var ratingFaker = new Faker<RatingDto>()
        .RuleFor(r => r.Rate, f => Math.Round(f.Random.Double(1.0, 5.0), 1))
        .RuleFor(r => r.Count, f => f.Random.Int(1, 1000));

        return new Faker<UpdateProductCommand>()
            .RuleFor(p=>p.Id, f=>Guid.NewGuid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(5, 500)))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Rating, f => ratingFaker.Generate());
}


    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static UpdateProductCommand GenerateValidCommand()
    {
        return UpdateProductHandlerFaker().Generate();
    }
    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static List<UpdateProductCommand> GenerateValidCommand(int count)
    {
        return UpdateProductHandlerFaker().Generate(count);
    }
}
