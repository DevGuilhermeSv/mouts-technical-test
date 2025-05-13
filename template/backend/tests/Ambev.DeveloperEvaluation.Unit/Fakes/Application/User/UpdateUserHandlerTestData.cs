using Ambev.DeveloperEvaluation.Application.Address;
using Ambev.DeveloperEvaluation.Application.Users;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Fakes.Application.User;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateUserHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static  Faker<UpdateUserCommand> UpdateUserHandlerFaker()
    {
        var addressValidator = new Faker<BaseAddressDto>()
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Number, f => f.Random.Int())
            .RuleFor(u => u.Street, f => f.Address.StreetName())
            .RuleFor(u => u.Zipcode, f => f.Address.ZipCode());
        return new Faker<UpdateUserCommand>()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
            .RuleFor(u => u.Name, f => new NameDto()
            {
                FirstName = f.Name.FirstName(),
                LastName = f.Name.LastName(),
            })
            .RuleFor(u => u.Address, f => addressValidator.Generate() );
    } 
    
    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static UpdateUserCommand GenerateValidCommand()
    {
        return UpdateUserHandlerFaker().Generate();
    }
}
