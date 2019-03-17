using System;
using System.Collections.Generic;
using System.Text;

namespace WingtipToys.Domain.Interfaces
{
    public interface IPageResult<T> where T: class
    {        
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPages { get; }
        int TotalCount { get; }
        IList<T> Items { get; }
    }
}
