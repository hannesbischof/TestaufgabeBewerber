using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Domain;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of products for the specified page.</returns>
        Task<IEnumerable<DomainProduct>> GetProducts(int pageNumber, int pageSize, string sortBy = null, string sortOrder = "asc", string filter = null);

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID, or null if not found.</returns>
        Task<DomainProduct> GetProductById(int id);

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        Task<DomainProduct> AddProduct(DomainProduct product);

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product with updated information.</param>
        /// <returns>The updated product.</returns>
        Task<DomainProduct> UpdateProduct(DomainProduct product);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteProduct(int id);
    }
}