using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Domain;
using Backend.Repositories;
using Backend.Services;
using Moq;
using Xunit;

namespace Backend.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _productService = new ProductService(_productRepositoryMock.Object, _categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsAddedProduct()
        {
            // Arrange
            var newProduct = new DomainProduct
            {
                Name = "Laptop",
                Description = "A high-performance laptop",
                Price = 1200.00m,
                Category = new DomainCategory { Id = 1 }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.GetCategoryById(newProduct.Category.Id))
                .ReturnsAsync(new DomainCategory { Id = 1, Name = "Electronics" });

            _productRepositoryMock
                .Setup(repo => repo.AddProduct(It.IsAny<DomainProduct>()))
                .ReturnsAsync(newProduct);

            // Act
            var result = await _productService.AddProduct(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newProduct.Name, result.Name);
            Assert.Equal(newProduct.Description, result.Description);
            Assert.Equal(newProduct.Price, result.Price);
            _categoryRepositoryMock.Verify(repo => repo.GetCategoryById(newProduct.Category.Id), Times.Once);
            _productRepositoryMock.Verify(repo => repo.AddProduct(It.IsAny<DomainProduct>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Valid description", 100, "Product name must be at least 5 characters long.")]
        [InlineData("Valid name", "", 100, "Product description must be at least 10 characters long.")]
        [InlineData("Valid name", "Short", 100, "Product description must be at least 10 characters long.")]
        [InlineData("Valid name", "Valid description", 0, "Product price must be greater than 0.")]
        public async Task AddProduct_InvalidProduct_ThrowsArgumentException(string name, string description, decimal price, string expectedMessage)
        {
            // Arrange
            var invalidProduct = new DomainProduct
            {
                Name = name,
                Description = description,
                Price = price,
                Category = new DomainCategory { Id = 1 }
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.AddProduct(invalidProduct));
            Assert.Equal(expectedMessage, exception.Message);
            _productRepositoryMock.Verify(repo => repo.AddProduct(It.IsAny<DomainProduct>()), Times.Never);
        }

        [Fact]
        public async Task UpdateProduct_ValidProduct_ReturnsUpdatedProduct()
        {
            // Arrange
            var existingProduct = new DomainProduct
            {
                Id = 1,
                Name = "Laptop",
                Description = "A high-performance laptop",
                Price = 1200.00m,
                Category = new DomainCategory { Id = 1 }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.GetCategoryById(existingProduct.Category.Id))
                .ReturnsAsync(new DomainCategory { Id = 1, Name = "Electronics" });

            _productRepositoryMock
                .Setup(repo => repo.UpdateProduct(It.IsAny<DomainProduct>()))
                .ReturnsAsync(existingProduct);

            // Act
            var result = await _productService.UpdateProduct(existingProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingProduct.Name, result.Name);
            Assert.Equal(existingProduct.Description, result.Description);
            Assert.Equal(existingProduct.Price, result.Price);
            _categoryRepositoryMock.Verify(repo => repo.GetCategoryById(existingProduct.Category.Id), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProduct(It.IsAny<DomainProduct>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Valid description", 100, "Product name must be at least 5 characters long.")]
        [InlineData("Valid name", "", 100, "Product description must be at least 10 characters long.")]
        [InlineData("Valid name", "Short", 100, "Product description must be at least 10 characters long.")]
        [InlineData("Valid name", "Valid description", 0, "Product price must be greater than 0.")]
        public async Task UpdateProduct_InvalidProduct_ThrowsArgumentException(string name, string description, decimal price, string expectedMessage)
        {
            // Arrange
            var invalidProduct = new DomainProduct
            {
                Id = 1,
                Name = name,
                Description = description,
                Price = price,
                Category = new DomainCategory { Id = 1 }
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.UpdateProduct(invalidProduct));
            Assert.Equal(expectedMessage, exception.Message);
            _productRepositoryMock.Verify(repo => repo.UpdateProduct(It.IsAny<DomainProduct>()), Times.Never);
        }

        [Fact]
        public async Task DeleteProduct_ValidId_DeletesProduct()
        {
            // Arrange
            var productId = 1;

            _productRepositoryMock
                .Setup(repo => repo.DeleteProduct(productId))
                .Returns(Task.CompletedTask);

            // Act
            await _productService.DeleteProduct(productId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteProduct(productId), Times.Once);
        }
    }
}
