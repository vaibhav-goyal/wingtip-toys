using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.API.Controllers;
using WingtipToys.Domain.Interfaces;
using WingtipToys.Domain.Models;
using Xunit;

namespace WingtipToys.API.Tests.Controllers
{
    public class ProductsControllerTests : IDisposable
    {
        private MockRepository mockRepository;

        private Mock<IProductRepository> mockProductRepository;

        private Mock<IPageResult<Product>> mockResult;

        public ProductsControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockResult = this.mockRepository.Create<IPageResult<Product>>();

            this.mockProductRepository = this.mockRepository.Create<IProductRepository>();
            this.mockProductRepository.Setup(repo => repo.GetProductsForCategoryAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(this.mockResult.Object);
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private ProductsController CreateProductsController()
        {
            return new ProductsController(
                this.mockProductRepository.Object);
        }

        [Fact]
        public async Task GetProducts__ReturnsOKResultWithPageResultObject()
        {
            // Arrange
            var unitUnderTest = this.CreateProductsController();
            int categoryID = 1;
            int pageIndex = 1;
            int pageSize = 1;
            CancellationToken cancelletionToken = default(CancellationToken);

            // Act
            var result = await unitUnderTest.GetProducts(
                categoryID,
                pageIndex,
                pageSize,
                cancelletionToken);

            // Assert
            Assert.True(result.GetType() ==  typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.True(okResult.StatusCode == 200);
            Assert.True(okResult.Value == this.mockResult.Object);
        }
    }
}
