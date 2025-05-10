using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a user's address
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Gets or sets the city name
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street name
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street number
    /// </summary>
    public int Number { get; set; }

    private ZipCode _zipCode;
    
    /// <summary>
    /// Gets or sets the ZIP code
    /// </summary>
    public string Zipcode
    {
        get => _zipCode.Value;
        set => _zipCode = new ZipCode(value);
    }

    /// <summary>
    /// Gets or sets the geolocation coordinates
    /// </summary>
    public Geolocation Geolocation { get; set; } = new();
    
    protected Address() { }
    
    public Address(string city, string street, int number,ZipCode zipCode)
    {
        _zipCode = zipCode;
    }
}

/// <summary>
/// Represents geographic coordinates
/// </summary>
public class Geolocation
{
    /// <summary>
    /// Gets or sets the latitude
    /// </summary>
    public string Lat { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the longitude
    /// </summary>
    public string Long { get; set; } = string.Empty;
}