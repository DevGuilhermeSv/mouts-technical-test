using System.Globalization;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class AddressFaker
    {
        public static Faker<Address> GetAddressFaker()
        {
            var zipCodeFaker = new Faker<ZipCode>()
                .CustomInstantiator(f => new ZipCode(f.Address.ZipCode()));

            return new Faker<Address>()
                .CustomInstantiator(f =>
                    new Address(
                        city: f.Address.City(),
                        street: f.Address.StreetName(),
                        number: f.Random.Int(1, 1000),
                        zipCode: zipCodeFaker.Generate()
                    )
                )
                .RuleFor(a => a.Geolocation, f => new Geolocation
                {
                    Lat = f.Address.Latitude().ToString(CultureInfo.CurrentCulture),
                    Long = f.Address.Longitude().ToString(CultureInfo.CurrentCulture)
                });
        }

        public static Address Generate() => GetAddressFaker().Generate();
        public static List<Address> GenerateMany(int count) => GetAddressFaker().Generate(count);
    }
}