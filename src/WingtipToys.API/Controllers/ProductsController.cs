using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.Domain.Interfaces;
using WingtipToys.Domain.Models;

namespace WingtipToys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IPageResult<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts([FromQuery]int categoryID = 1, [FromQuery]int pageIndex = 1, [FromQuery]int pageSize = 20, CancellationToken cancelletionToken = default(CancellationToken))
        {
            var result = await _productRepository.GetProductsForCategoryAsync(categoryID, pageIndex, pageSize, cancelletionToken);
            return Ok(result);
        }
    }
}