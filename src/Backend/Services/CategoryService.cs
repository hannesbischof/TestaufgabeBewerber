using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services
{
    /// <summary>
    /// Service class implementing business logic for categories.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Category>> GetCategories(int pageNumber, int pageSize)
        {
            return await _categoryRepository.GetCategories(pageNumber, pageSize);
        }

        /// <inheritdoc />
        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        /// <inheritdoc />
        public async Task<Category> AddCategory(Category category)
        {
            ValidateCategory(category);
            return await _categoryRepository.AddCategory(category);
        }

        /// <inheritdoc />
        public async Task<Category> UpdateCategory(Category category)
        {
            ValidateCategory(category);
            return await _categoryRepository.UpdateCategory(category);
        }

        /// <inheritdoc />
        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategory(id);
        }

        /// <summary>
        /// Validates the category's properties.
        /// </summary>
        /// <param name="category">The category to validate.</param>
        private void ValidateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name) || category.Name.Length > 50)
            {
                throw new ArgumentException("Category name must not be empty and must not exceed 50 characters.");
            }

            if (string.IsNullOrWhiteSpace(category.Description) || category.Description.Length > 200)
            {
                throw new ArgumentException("Category description must not be empty and must not exceed 200 characters.");
            }
        }
    }
}