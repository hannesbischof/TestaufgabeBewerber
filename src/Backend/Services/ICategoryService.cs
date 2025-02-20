using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Domain;

namespace Backend.Services
{
    /// <summary>
    /// Interface to abstract business logic for categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves a paginated list of categories.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of categories for the specified page.</returns>
        Task<IEnumerable<DomainCategory>> GetCategories(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID, or null if not found.</returns>
        Task<DomainCategory> GetCategoryById(int id);

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns>The added category.</returns>
        Task<DomainCategory> AddCategory(DomainCategory category);

        /// <summary>
        /// Updates an existing category in the database.
        /// </summary>
        /// <param name="category">The category with updated information.</param>
        /// <returns>The updated category.</returns>
        Task<DomainCategory> UpdateCategory(DomainCategory category);

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteCategory(int id);
    }
}