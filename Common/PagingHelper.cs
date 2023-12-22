using Common.Pagging;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class PagingHelper
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, PagingRequest page)
        {
            var methodName = page.SortDir == EnumHelper<SortOrder>.GetDisplayValue(SortOrder.Asc) ? "OrderBy" : "OrderByDescending";
            return methodName.ToUpper() == "OrderBy".ToUpper() ? enumerable.OrderBy(x => GetProperty(x, page.SortField)) : enumerable.OrderByDescending(x => GetProperty(x, page.SortField));
        }
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> records, PagingRequest page) where T : class
        {
            if (!page.PageSize.HasValue || page.PageSize == 0)
                page.PageSize = 10;

            page.Page ??= 0;

            if (page.Page.Value > 0)
            {
                page.Page--;
                page.Page = page.Page.Value * page.PageSize.Value;
            }
            return records.Skip(page.Page.Value).Take(page.PageSize.Value);
        }
        private static object GetProperty(object o, string propertyName)
        {
            return o.GetType().GetProperty(propertyName)?.GetValue(o, null);
        }
    }
}
