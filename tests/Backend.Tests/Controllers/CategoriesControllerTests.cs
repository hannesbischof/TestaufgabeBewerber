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

namespace Backend.Tests.Controllers
{
    public class CategoriesControllerTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly AppDbContext _context;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
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
            _controller = new CategoriesController(
                new TestMediator(_context),
                new TestMapper()
            );
        }

        private void SeedDatabase()
        {
            _context.Categories.AddRange(
                new Category { Name = "Electronics", Description = "Devices and gadgets" },
                new Category { Name = "Books", Description = "Printed and digital books" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var categories = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
            Assert.Equal(2, categories.Count());
        }

        [Fact]
        public async Task GetCategoryById_ValidId_ReturnsCategory()
        {
            // Arrange
            var categoryId = _context.Categories.First().Id;

            // Act
            var result = await _controller.GetCategoryById(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var category = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("Electronics", category.Name);
        }

        [Fact]
        public async Task GetCategoryById_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetCategoryById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddCategory_ValidCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var newCategory = new Category
            {
                Name = "Clothing",
                Description = "Apparel and accessories"
            };

            // Act
            var result = await _controller.AddCategory(newCategory);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var category = Assert.IsType<Category>(createdResult.Value);
            Assert.Equal("Clothing", category.Name);
            Assert.Equal(3, _context.Categories.Count());
        }

        [Fact]
        public async Task AddCategory_InvalidCategory_ReturnsBadRequest()
        {
            // Arrange
            var invalidCategory = new Category
            {
                Name = "", // Invalid name
                Description = "Invalid category"
            };

            // Act
            var result = await _controller.AddCategory(invalidCategory);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateCategory_ValidCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var existingCategory = _context.Categories.First();
            existingCategory.Name = "Updated Electronics";

            // Act
            var result = await _controller.UpdateCategory(existingCategory.Id, existingCategory);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var category = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("Updated Electronics", category.Name);
        }

        [Fact]
        public async Task UpdateCategory_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var existingCategory = _context.Categories.First();
            existingCategory.Id = 999; // Invalid ID

            // Act
            var result = await _controller.UpdateCategory(1, existingCategory);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCategory_ValidId_ReturnsNoContent()
        {
            // Arrange
            var categoryId = _context.Categories.First().Id;

            // Act
            var result = await _controller.DeleteCategory(categoryId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(1, _context.Categories.Count());
        }

        [Fact]
        public async Task DeleteCategory_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.DeleteCategory(999);

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
