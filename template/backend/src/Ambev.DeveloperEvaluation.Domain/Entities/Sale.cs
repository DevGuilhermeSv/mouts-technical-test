using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction made by a customer at a specific branch.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    ///
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleNumber { get; private set; }

    /// <summary>
    /// The date and time the sale was made.
    /// </summary>
    public DateTime SaleDate { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// The name or identifier of the customer.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// The branch where the sale was made.
    /// </summary>
    public string Branch { get; private set; }

    /// <summary>
    /// Indicates whether the sale was cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// The total amount of the sale.
    /// </summary>
    public decimal TotalAmount => Items.Sum(i => i.Total);

    /// <summary>
    /// The list of items sold in this sale.
    /// </summary>
    public List<SaleItem> Items { get; private set; } = new();

    protected Sale() { } // For EF

    public Sale(DateTime saleDate, Guid userId, string branch, List<SaleItem> items)
    {
        SaleDate = saleDate;
        UserId = userId;
        Branch = branch ?? throw new ArgumentNullException(nameof(branch));
        Items = items ?? throw new ArgumentNullException(nameof(items));

        if (!Items.Any())
            throw new ArgumentException("A sale must contain at least one item.", nameof(items));
    }

    public void Cancel()
    {
        IsCancelled = true;
    }
}