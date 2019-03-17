using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.Domain.Models;

namespace WingtipToys.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IPageResult<Product>> GetProductsForCategoryAsync(int categoryID, int pageNo = 1, int pageSize = 20, CancellationToken cancellationToken = default(CancellationToken));
    }
}
