using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents a valid ZIP code
/// </summary>
public class ZipCode
{
    private static readonly Regex ZipCodeRegex = new(@"^\d{5}-?\d{3}$");

    public string Value { get; }

    public ZipCode(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid ZIP code format", nameof(value));

        Value = value;
    }

    public static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && ZipCodeRegex.IsMatch(value);
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) => obj is ZipCode other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}