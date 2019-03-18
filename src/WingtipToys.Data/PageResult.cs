using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.Domain.Exceptions;
using WingtipToys.Domain.Interfaces;

namespace WingtipToys.Data
{
    public class PageResult<T> : IPageResult<T> where T : class
    {

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public IList<T> Items { get; set; }

    }

    public static class IQueryablePageResultExtensions
    {
        public static async Task<IPageResult<T>> ToPagedResultAsync<T>(this IQueryable<T> source, int pageNo, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            if (pageNo < 1)
                throw new DomainException("Page No cannot be less than 1.", new ArgumentOutOfRangeException(nameof(pageNo)));
            if (pageSize < 1)
                throw new DomainException("Page Size cannot be less than 1.", new ArgumentOutOfRangeException(nameof(pageSize)));

            var totalCount = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            if (pageNo > totalPages && totalCount > 0)
                throw new DomainException($"Page No cannot be more than {totalPages}", new ArgumentOutOfRangeException(nameof(pageNo)));

            var skip = (pageNo - 1) * pageSize;
            var items = await source.Skip(skip)
                                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            return new PageResult<T>
            {
                Items = items,
                PageNo = pageNo,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };




        }
    }
}
