using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend;
using Backend.Models;
using Backend.Models.Domain;
using Backend.Controllers;
using Microsoft.Data.Sqlite;
using Xunit;
using System.Threading;
using Backend.Mediator;

namespace Backend.Tests.Controllers
{
    public class ProductsControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly AppDbContext _context;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            // Setup in-memory SQLite database
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new AppDbContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            // Seed data
            SeedDatabase();

            // Initialize controller
            _controller = new ProductsController(
                new TestMediator(_context),
                new TestMapper()
            );
        }

        private void SeedDatabase()
        {
            var category = new Category { Name = "Electronics", Description = "Devices and gadgets" };
            _context.Categories.Add(category);
            _context.Products.AddRange(
                new Product { Name = "Laptop", Description = "A high-performance laptop", Price = 1200.00m, Category = category },
                new Product { Name = "Smartphone", Description = "A modern smartphone", Price = 800.00m, Category = category }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public async Task GetProductById_ValidId_ReturnsProduct()
        {
            // Arrange
            var productId = _context.Products.First().Id;

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal("Laptop", product.Name);
        }

        [Fact]
        public async Task GetProductById_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetProductById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsCreatedProduct()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Tablet",
                Description = "A lightweight tablet",
                Price = 500.00m,
                Category = _context.Categories.First()
            };

            // Act
            var result = await _controller.AddProduct(newProduct);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var product = Assert.IsType<Product>(createdResult.Value);
            Assert.Equal("Tablet", product.Name);
            Assert.Equal(3, _context.Products.Count());
        }

        [Fact]
        public async Task AddProduct_InvalidProduct_ReturnsBadRequest()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Name = "", // Invalid name
                Description = "Invalid product",
                Price = 100.00m,
                Category = _context.Categories.First()
            };

            // Act
            var result = await _controller.AddProduct(invalidProduct);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProduct_ValidProduct_ReturnsUpdatedProduct()
        {
            // Arrange
            var existingProduct = _context.Products.First();
            existingProduct.Name = "Updated Laptop";

            // Act
            var result = await _controller.UpdateProduct(existingProduct.Id, existingProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal("Updated Laptop", product.Name);
        }

        [Fact]
        public async Task UpdateProduct_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var existingProduct = _context.Products.First();
            existingProduct.Id = 999; // Invalid ID

            // Act
            var result = await _controller.UpdateProduct(1, existingProduct);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProduct_ValidId_ReturnsNoContent()
        {
            // Arrange
            var productId = _context.Products.First().Id;

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(1, _context.Products.Count());
        }

        [Fact]
        public async Task DeleteProduct_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.DeleteProduct(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Close();
        }
    }

    // Mock implementations for testing
    public class TestMediator : IMediator
    {
        private readonly AppDbContext _context;

        public TestMediator(AppDbContext context)
        {
            _context = context;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            throw new NotImplementedException();
        }
    }

    public class TestMapper : IMapper
    {
        public TDestination Map<TDestination>(object source)
        {
            return (TDestination)source;
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return destination;
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return source;
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return destination;
        }
    }
}
