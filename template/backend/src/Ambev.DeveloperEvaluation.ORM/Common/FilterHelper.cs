using Microsoft.IdentityModel.Tokens;

namespace Ambev.DeveloperEvaluation.ORM.Common;
using System.Linq.Dynamic.Core;

public static class FilterHelper
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, string? filter)
    {
        if (string.IsNullOrEmpty(filter)) return queryable;
        if (filter.StartsWith("*"))
        {
            string likeExpression = $"{property}.EndsWith(@0)";
            queryable = queryable.Where(likeExpression, filter.TrimStart('*'));
        }
        else if (filter.EndsWith("*"))
        {
            string likeExpression = $"{property}.StartsWith(@0)";
            queryable = queryable.Where(likeExpression, filter.TrimEnd('*'));
        }
        else
        {
            string likeExpression = $"{property} == @0";
            queryable = queryable.Where(likeExpression, filter);
        }
        return queryable;
    }
    public static IQueryable<T> FilterNumeric<T>(this IQueryable<T> queryable, string property, double? filter)
    {
        if (filter.HasValue)
        {
            if (property.StartsWith("_min"))
            {
                string baseProperty = property.Substring(0, 4);
                string expression = $"{baseProperty} >= @0";
                queryable = queryable.Where(expression, filter.Value);
            }
            else if (property.StartsWith("_max"))
            {
                string baseProperty = property.Substring(0, 4);
                string expression = $"{baseProperty} <= @0";
                queryable = queryable.Where(expression, filter.Value);
            }
            else
            {
                string expression = $"{property} == @0";
                queryable = queryable.Where(expression, filter.Value);
            }
        }
        return queryable;
    }
}