using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Image { get; private set; }
    public Rating Rating { get; private set; }

    protected Product() { } 

    public Product(
        string title,
        decimal price,
        string description,
        string category,
        string image,
        Rating rating)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetPrice(price);
        SetDescription(description);
        SetCategory(category);
        SetImage(image);
        SetRating(rating);
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title.Trim();
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");

        Price = price;
    }

    public void SetDescription(string description)
    {
        Description = description?.Trim() ?? string.Empty;
    }

    public void SetCategory(string category)
    {
        Category = string.IsNullOrWhiteSpace(category)
            ? "Uncategorized"
            : category.Trim();
    }

    public void SetImage(string image)
    {
        Image = image?.Trim() ?? string.Empty;
    }

    public void SetRating(Rating rating)
    {
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
    }
}
