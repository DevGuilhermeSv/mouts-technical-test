using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products;

/// <summary>
/// Represents a base data transfer object for a product.
/// </summary>
public class BaseProductDto
{
    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating information of the product.
    /// </summary>
    public RatingDto Rating { get; set; } = new RatingDto();
}

/// <summary>
/// Represents the rating information for a product.
/// </summary>
public class RatingDto
{
    /// <summary>
    /// Gets or sets the rating value of the product.
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// Gets or sets the number of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
