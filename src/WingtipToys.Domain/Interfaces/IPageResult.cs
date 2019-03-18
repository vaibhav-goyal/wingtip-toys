using System.Collections.Generic;

namespace WingtipToys.Domain.Interfaces
{
    public interface IPageResult<T> where T : class
    {
        int PageNo { get; }

        int PageSize { get; }

        int TotalPages { get;}

        int TotalCount { get; }

        IList<T> Items { get; }
    }
}