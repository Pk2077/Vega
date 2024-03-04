using System.Linq.Expressions;
using Vega.Models;

namespace Vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, Dictionary<string, Expression<Func<T, object>>> columnMap, IQueryObject queryObj)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortBy) || !columnMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnMap[queryObj.SortBy]);
        }


        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query,IQueryObject queryObj)
        {
            if (queryObj.PageSize <= 0)
                 queryObj.PageSize = 10;
            if (queryObj.Page <= 0)
                 queryObj.Page = 1;

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}
