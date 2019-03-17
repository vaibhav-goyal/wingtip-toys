using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.Domain.Interfaces;

namespace WingtipToys.Data
{
    public class PageResult<T> : IPageResult<T> where T : class
    {
        internal PageResult(IList<T> items, int pageIndex, int pageSize, int totalCount)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (pageIndex < 1)
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            PageIndex = pageIndex;
            PageSize = pageSize;           
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = items;
        }

        public int PageIndex { get; protected set; }

        public int PageSize { get; protected set; }

        public int TotalPages { get; protected set; }

        public int TotalCount { get; protected set; }

        public IList<T> Items { get; protected set; }

    }

    public static class IQueryablePageResultExtensions
    {
        public static async Task<IPageResult<T>> ToPagedResultAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T: class
        {
           

            var totalCount = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            var pagedList = new PageResult<T>(items, pageIndex, pageSize, totalCount);
            

            return pagedList;
        }
    }
}
