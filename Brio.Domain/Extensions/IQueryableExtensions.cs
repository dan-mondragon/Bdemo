using Brio.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brio.Domain.Extensions
{
    public static class IQueryableExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, PagingParameter pagingParameter) where T : class
        {
            int page = pagingParameter.pageNumber;
            int pageSize = pagingParameter.pageSize;
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.Total = query.Count();
            result.TotalPages = (int)Math.Ceiling(result.Total / (double)pageSize);
            result.PreviousPage = page > 1 ? "Yes" : "No";
            result.NextPage = page < result.TotalPages ? "Yes" : "No";

            var pageCount = (double)result.Total / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            if (!string.IsNullOrEmpty(pagingParameter.Sort))
            {
                string sort = pagingParameter.Sort.StartsWith("-") ? pagingParameter.Sort.Substring(1) : pagingParameter.Sort;
                var type = typeof(T);
                var propertyInfo = type.GetProperty(sort);
                if(propertyInfo != null)
                {
                    if (pagingParameter.Sort.StartsWith("-"))
                        query = query.OrderByDescending(x => propertyInfo.GetValue(x, null));
                    else
                        query = query.OrderBy(x => propertyInfo.GetValue(x, null));
                }                
            }

            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
