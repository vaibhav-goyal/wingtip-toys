using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WingtipToys.Domain.Exceptions;
using WingtipToys.Domain.Interfaces;
using WingtipToys.Domain.Models;
using Xunit;

namespace WingtipToys.Data.Tests
{
    public class ProductRepositoryTests : IDisposable
    {
        private SqliteConnection _connection;
        private WingtiptoysContext _context;

        public ProductRepositoryTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<WingtiptoysContext>()
                    .UseSqlite(_connection)
                    .Options;

            _context = new WingtiptoysContext(options);
            _context.Database.EnsureCreated();

            _context.Categories.Add(new Category() { CategoryId = 1, CategoryName = "N1", Description = "D" });
            _context.Categories.Add(new Category() { CategoryId = 2, CategoryName = "N2", Description = "D" });

            _context.Products.Add(new Product() { CategoryId = 1, ProductName = "ProductName1", Description = "D" });
            _context.Products.Add(new Product() { CategoryId = 1, ProductName = "ProductName2", Description = "D" });
            _context.Products.Add(new Product() { CategoryId = 1, ProductName = "ProductName3", Description = "D" });
            _context.Products.Add(new Product() { CategoryId = 1, ProductName = "ProductName4", Description = "D" });
            _context.Products.Add(new Product() { CategoryId = 1, ProductName = "ProductName5", Description = "D" });
            _context.Products.Add(new Product() { CategoryId = 2, ProductName = "ProductName6", Description = "D" });
            _context.SaveChanges();

        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        [Theory]
        [InlineData(1, 1, 1, 5, 5, 1)]
        [InlineData(1, 5, 1, 5, 5, 1)]
        [InlineData(1, 3, 2, 5, 3, 1)]
        [InlineData(2, 1, 1, 1, 1, 1)]
        [InlineData(3, 1, 1, 0, 0, 0)]
        public async Task GetProductsForCategoryAsync_PositiveTests(int categoryID, int pageNo, int pageSize, int expectedTotalCount, int expectedTotalPages, int expectedItemCount)
        {

            CancellationToken cancellationToken = default(CancellationToken);

            var repo = new ProductRepository(_context);
            IPageResult<Product> result = await repo.GetProductsForCategoryAsync(categoryID, pageNo, pageSize, cancellationToken);
            Assert.True(result.PageSize == pageSize);
            Assert.True(result.PageNo == pageNo);
            Assert.True(result.TotalCount == expectedTotalCount);
            Assert.True(result.TotalPages == expectedTotalPages);
            Assert.True(result.Items.Count == expectedItemCount);

        }

        [Theory]
        [InlineData(1, -1, 1, typeof(DomainException))]
        [InlineData(1, 1, -1, typeof(DomainException))]
        [InlineData(1, 6, 1, typeof(DomainException))]
        public async Task GetProductsForCategoryAsync_NegativeTests(int categoryID, int pageNo, int pageSize, Type exceptionType)
        {

            CancellationToken cancellationToken = default(CancellationToken);

            var repo = new ProductRepository(_context);
            await Assert.ThrowsAsync(exceptionType,() => { return repo.GetProductsForCategoryAsync(categoryID, pageNo, pageSize, cancellationToken); });            
        }
    }
}
