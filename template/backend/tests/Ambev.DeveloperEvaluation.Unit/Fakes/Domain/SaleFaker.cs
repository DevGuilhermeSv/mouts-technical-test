using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class SaleFaker
{
    public static Faker<Sale> GetSaleFaker()
    {
        var saleItemFaker = SaleItemFaker.GetSaleItemFaker();

        return new Faker<Sale>()
            .CustomInstantiator(f =>
                new Sale(
                    saleDate: f.Date.RecentOffset(30).UtcDateTime,
                    userId: f.Random.Guid(),
                    branch: f.Company.CompanyName(),
                    items: saleItemFaker.Generate(f.Random.Int(1, 5))
                )
            );
    }

    public static Sale Generate() => GetSaleFaker().Generate();
    public static List<Sale> GenerateMany(int count) => GetSaleFaker().Generate(count);
}

public static class SaleItemFaker
{
    public static Faker<SaleItem> GetSaleItemFaker()
    {
        return new Faker<SaleItem>()
            .CustomInstantiator(f =>
            {
                var product = ProductFaker.Generate();
                var quantity = f.Random.Int(1, 10);

                return new SaleItem(product.Id, quantity,f.Random.Decimal(0,1000));
            });
    }

    public static SaleItem Generate() => GetSaleItemFaker().Generate();
    public static List<SaleItem> GenerateMany(int count) => GetSaleItemFaker().Generate(count);
}