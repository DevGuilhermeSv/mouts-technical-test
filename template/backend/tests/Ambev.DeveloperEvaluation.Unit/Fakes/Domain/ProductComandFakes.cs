using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class ProductFaker
    {
        public static Faker<Product> GetProductFaker()
        {
            var ratingFaker = new Faker<Rating>()
                .CustomInstantiator(f => new Rating(
                    rate: (double)f.Random.Decimal(0, 5),
                    count: f.Random.Int(1, 1000)
                ));

            return new Faker<Product>()
                .CustomInstantiator(f =>
                    new Product(
                        title: f.Commerce.ProductName(),
                        price: f.Random.Decimal(1.00m, 1000.00m),
                        description: f.Commerce.ProductDescription(),
                        category: f.Commerce.Categories(1)[0],
                        image: f.Image.PicsumUrl(),
                        rating: ratingFaker.Generate()
                    )
                );
        }

        public static Product Generate() => GetProductFaker().Generate();

        public static List<Product> GenerateMany(int count) => GetProductFaker().Generate(count);
    }
}