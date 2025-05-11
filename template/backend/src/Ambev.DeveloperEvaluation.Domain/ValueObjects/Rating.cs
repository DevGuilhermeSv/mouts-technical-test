namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Rating
{
    public double Rate { get; private set; }
    public int Count { get; private set; }

    public Rating(double rate, int count)
    {
        if (rate < 0 || rate > 5)
            throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 5.");

        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");

        Rate = rate;
        Count = count;
    }

    public Rating Update(double newRate, int newCount)
    {
        return new Rating(newRate, newCount);
    }

    public override bool Equals(object? obj)
    {
        return obj is Rating other &&
               Rate.Equals(other.Rate) &&
               Count == other.Count;
    }

    public override int GetHashCode() => HashCode.Combine(Rate, Count);
}
