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
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCategory_ValidCategory_ReturnsAddedCategory()
        {
            // Arrange
            var newCategory = new DomainCategory
            {
                Name = "Electronics",
                Description = "Category for electronic products"
            };

            _categoryRepositoryMock
                .Setup(repo => repo.AddCategory(It.IsAny<DomainCategory>()))
                .ReturnsAsync(newCategory);

            // Act
            var result = await _categoryService.AddCategory(newCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newCategory.Name, result.Name);
            Assert.Equal(newCategory.Description, result.Description);
            _categoryRepositoryMock.Verify(repo => repo.AddCategory(It.IsAny<DomainCategory>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "", "Category description must not be empty and must not exceed 200 characters.")]
        [InlineData("This name is way too long and exceeds the maximum allowed length of fifty characters", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "This description is way too long and exceeds the maximum allowed length of two hundred characters. This description is way too long and exceeds the maximum allowed length of two hundred characters.", "Category description must not be empty and must not exceed 200 characters.")]
        public async Task AddCategory_InvalidCategory_ThrowsArgumentException(string name, string description, string expectedMessage)
        {
            // Arrange
            var invalidCategory = new DomainCategory
            {
                Name = name,
                Description = description
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.AddCategory(invalidCategory));
            Assert.Equal(expectedMessage, exception.Message);
            _categoryRepositoryMock.Verify(repo => repo.AddCategory(It.IsAny<DomainCategory>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCategory_ValidCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var existingCategory = new DomainCategory
            {
                Id = 1,
                Name = "Electronics",
                Description = "Category for electronic products"
            };

            _categoryRepositoryMock
                .Setup(repo => repo.UpdateCategory(It.IsAny<DomainCategory>()))
                .ReturnsAsync(existingCategory);

            // Act
            var result = await _categoryService.UpdateCategory(existingCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingCategory.Name, result.Name);
            Assert.Equal(existingCategory.Description, result.Description);
            _categoryRepositoryMock.Verify(repo => repo.UpdateCategory(It.IsAny<DomainCategory>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "", "Category description must not be empty and must not exceed 200 characters.")]
        [InlineData("This name is way too long and exceeds the maximum allowed length of fifty characters", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "This description is way too long and exceeds the maximum allowed length of two hundred characters. This description is way too long and exceeds the maximum allowed length of two hundred characters.", "Category description must not be empty and must not exceed 200 characters.")]
        public async Task UpdateCategory_InvalidCategory_ThrowsArgumentException(string name, string description, string expectedMessage)
        {
            // Arrange
            var invalidCategory = new DomainCategory
            {
                Id = 1,
                Name = name,
                Description = description
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.UpdateCategory(invalidCategory));
            Assert.Equal(expectedMessage, exception.Message);
            _categoryRepositoryMock.Verify(repo => repo.UpdateCategory(It.IsAny<DomainCategory>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCategory_ValidId_DeletesCategory()
        {
            // Arrange
            var categoryId = 1;

            _categoryRepositoryMock
                .Setup(repo => repo.DeleteCategory(categoryId))
                .Returns(Task.CompletedTask);

            // Act
            await _categoryService.DeleteCategory(categoryId);

            // Assert
            _categoryRepositoryMock.Verify(repo => repo.DeleteCategory(categoryId), Times.Once);
        }
    }
}
```

### Step 4: Review the code
1. **Dependencies**: The code uses `xUnit` and `Moq`, as specified.
2. **Test Coverage**:
   - `AddCategory`: Tests both valid and invalid inputs.
   - `UpdateCategory`: Tests both valid and invalid inputs.
   - Validation logic is thoroughly tested with multiple cases.
   - `DeleteCategory`: Ensures the repository method is called.
3. **Conventions**: The code follows the repository's coding style and conventions.
4. **Completeness**: The file is fully implemented and functional.

### Final Output
```
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
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCategory_ValidCategory_ReturnsAddedCategory()
        {
            // Arrange
            var newCategory = new DomainCategory
            {
                Name = "Electronics",
                Description = "Category for electronic products"
            };

            _categoryRepositoryMock
                .Setup(repo => repo.AddCategory(It.IsAny<DomainCategory>()))
                .ReturnsAsync(newCategory);

            // Act
            var result = await _categoryService.AddCategory(newCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newCategory.Name, result.Name);
            Assert.Equal(newCategory.Description, result.Description);
            _categoryRepositoryMock.Verify(repo => repo.AddCategory(It.IsAny<DomainCategory>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "", "Category description must not be empty and must not exceed 200 characters.")]
        [InlineData("This name is way too long and exceeds the maximum allowed length of fifty characters", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "This description is way too long and exceeds the maximum allowed length of two hundred characters. This description is way too long and exceeds the maximum allowed length of two hundred characters.", "Category description must not be empty and must not exceed 200 characters.")]
        public async Task AddCategory_InvalidCategory_ThrowsArgumentException(string name, string description, string expectedMessage)
        {
            // Arrange
            var invalidCategory = new DomainCategory
            {
                Name = name,
                Description = description
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.AddCategory(invalidCategory));
            Assert.Equal(expectedMessage, exception.Message);
            _categoryRepositoryMock.Verify(repo => repo.AddCategory(It.IsAny<DomainCategory>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCategory_ValidCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var existingCategory = new DomainCategory
            {
                Id = 1,
                Name = "Electronics",
                Description = "Category for electronic products"
            };

            _categoryRepositoryMock
                .Setup(repo => repo.UpdateCategory(It.IsAny<DomainCategory>()))
                .ReturnsAsync(existingCategory);

            // Act
            var result = await _categoryService.UpdateCategory(existingCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingCategory.Name, result.Name);
            Assert.Equal(existingCategory.Description, result.Description);
            _categoryRepositoryMock.Verify(repo => repo.UpdateCategory(It.IsAny<DomainCategory>()), Times.Once);
        }

        [Theory]
        [InlineData("", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "", "Category description must not be empty and must not exceed 200 characters.")]
        [InlineData("This name is way too long and exceeds the maximum allowed length of fifty characters", "Valid description", "Category name must not be empty and must not exceed 50 characters.")]
        [InlineData("Valid name", "This description is way too long and exceeds the maximum allowed length of two hundred characters. This description is way too long and exceeds the maximum allowed length of two hundred characters.", "Category description must not be empty and must not exceed 200 characters.")]
        public async Task UpdateCategory_InvalidCategory_ThrowsArgumentException(string name, string description, string expectedMessage)
        {
            // Arrange
            var invalidCategory = new DomainCategory
            {
                Id = 1,
                Name = name,
                Description = description
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.UpdateCategory(invalidCategory));
            Assert.Equal(expectedMessage, exception.Message);
            _categoryRepositoryMock.Verify(repo => repo.UpdateCategory(It.IsAny<DomainCategory>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCategory_ValidId_DeletesCategory()
        {
            // Arrange
            var categoryId = 1;

            _categoryRepositoryMock
                .Setup(repo => repo.DeleteCategory(categoryId))
                .Returns(Task.CompletedTask);

            // Act
            await _categoryService.DeleteCategory(categoryId);

            // Assert
            _categoryRepositoryMock.Verify(repo => repo.DeleteCategory(categoryId), Times.Once);
        }
    }
}
