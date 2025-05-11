namespace Ambev.DeveloperEvaluation.Application.Address;

/// <summary>
/// Data Transfer Object representing a user's address
/// </summary>
public class BaseAddressDto
{
    /// <summary>
    /// Gets or sets the city name
    /// </summary>
    public required string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the street name
    /// </summary>
    public required string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the house/building number
    /// </summary>
    public required int Number { get; set; }

    /// <summary>
    /// Gets or sets the ZIP code
    /// </summary>
    public required string Zipcode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographic coordinates
    /// </summary>
    public GeolocationDto? Geolocation { get; set; } = new();
}
/// <summary>
/// Data Transfer Object representing geolocation coordinates
/// </summary>
public class GeolocationDto
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
