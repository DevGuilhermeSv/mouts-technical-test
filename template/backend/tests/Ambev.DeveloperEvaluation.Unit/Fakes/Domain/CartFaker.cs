using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CartFaker
    {
        private static Faker<Cart> GetCartFaker()
        {
            var cartProductFaker = CartProductFaker.GetCartProductFaker();

            return new Faker<Cart>()
                .CustomInstantiator(f =>
                    new Cart(
                        userId: f.Random.Guid(),
                        date: f.Date.RecentOffset(10).UtcDateTime,
                        products: cartProductFaker.Generate(f.Random.Int(1, 5))
                    )
                );
        }

        public static Cart Generate() => GetCartFaker().Generate();
        public static List<Cart> GenerateMany(int count) => GetCartFaker().Generate(count);
    }

    public static class CartProductFaker
    {
        public static Faker<CartProduct> GetCartProductFaker()
        {
            return new Faker<CartProduct>()
                .CustomInstantiator(f =>
                {
                    var product = ProductFaker.Generate();
                    var quantity = f.Random.Int(1, 10);

                    return new CartProduct(product.Id, quantity,null);
                });
        }

        public static CartProduct Generate() => GetCartProductFaker().Generate();
        public static List<CartProduct> GenerateMany(int count) => GetCartProductFaker().Generate(count);
    }
}