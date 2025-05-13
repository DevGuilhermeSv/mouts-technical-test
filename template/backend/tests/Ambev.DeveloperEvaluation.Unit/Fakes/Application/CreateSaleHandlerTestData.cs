using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Fakes.Application;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// - ProductId
    /// - Quantity
    /// - UnitPrice
    /// - SaleDate
    /// - CustomerId
    /// - Branch
    /// - Items
    /// </summary>
    private static Faker<CreateSaleCommand> CreateSaleHandlerFaker()
    {
        var saleItemFaker = new Faker<SaleItemDto>()
            .RuleFor(i => i.ProductId, f => f.Random.Guid())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price(10, 200)));

        return new Faker<CreateSaleCommand>()
            .RuleFor(s => s.SaleDate, f => f.Date.RecentOffset(10).UtcDateTime)
            .RuleFor(s => s.CustomerId, f => f.Random.Guid())
            .RuleFor(s => s.Branch, f => f.Company.CompanyName())
            .RuleFor(s => s.Items, f => saleItemFaker.Generate(f.Random.Int(1, 5)));
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleHandlerFaker().Generate();
    }
    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static List<CreateSaleCommand> GenerateValidCommand(int count)
    {
        return CreateSaleHandlerFaker().Generate(count);
    }
}
