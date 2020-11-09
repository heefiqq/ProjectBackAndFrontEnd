using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBackAndFrontend.Core.Extensions
{
    public static class QueryExtensions
    {
        public static Paging.FilterResult<T> ToPagedList<T>(this IEnumerable<T> items, int currentPageIndex, int itemsPerPage)
        {
            var model = new Paging.FilterResult<T>();

            model.PageIndex = currentPageIndex;
            model.TotalItemsCount = items.Count();
            model.TotalPageCount = (int)(Math.Ceiling((double)model.TotalItemsCount / itemsPerPage));

            if (model.TotalPageCount < currentPageIndex && currentPageIndex > 1)
            {
                return model;
            }

            if (currentPageIndex > 1)
                items = items.Skip((currentPageIndex - 1) * itemsPerPage);

            model.DataItems = items.Take(itemsPerPage).ToList();

            return model;
        }
    }
}
