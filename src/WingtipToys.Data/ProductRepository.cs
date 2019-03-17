using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.Domain.Interfaces;
using WingtipToys.Domain.Models;

namespace WingtipToys.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly WingtiptoysContext _dbContext;

        public ProductRepository(WingtiptoysContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IPageResult<Product>> GetProductsForCategoryAsync(int categoryID, int pageNo = 1, int pageSize = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _dbContext.Products.Where(p => p.CategoryId == categoryID).ToPagedResultAsync(pageNo, pageSize,cancellationToken);
            return result;
        }
    }
}
