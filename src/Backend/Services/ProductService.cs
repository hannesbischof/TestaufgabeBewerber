using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Domain;
using Backend.Repositories;

namespace Backend.Services
{
    /// <summary>
    /// Service class implementing business logic for products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DomainProduct>> GetProducts(int pageNumber, int pageSize)
        {
            return await _productRepository.GetProducts(pageNumber, pageSize);
        }

        /// <inheritdoc />
        public async Task<DomainProduct> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        /// <inheritdoc />
        public async Task<DomainProduct> AddProduct(DomainProduct product)
        {
            ValidateProduct(product);

            // Ensure the category exists
            var category = await _categoryRepository.GetCategoryById(product.Category.Id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {product.Category.Id} does not exist.");
            }

            return await _productRepository.AddProduct(product);
        }

        /// <inheritdoc />
        public async Task<DomainProduct> UpdateProduct(DomainProduct product)
        {
            ValidateProduct(product);

            // Ensure the category exists
            var category = await _categoryRepository.GetCategoryById(product.Category.Id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {product.Category.Id} does not exist.");
            }

            return await _productRepository.UpdateProduct(product);
        }

        /// <inheritdoc />
        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }

        /// <summary>
        /// Validates the product's properties.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        private void ValidateProduct(DomainProduct product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Name.Length < 5)
            {
                throw new ArgumentException("Product name must be at least 5 characters long.");
            }

            if (string.IsNullOrWhiteSpace(product.Description) || product.Description.Length < 10)
            {
                throw new ArgumentException("Product description must be at least 10 characters long.");
            }

            if (product.Price <= 0)
            {
                throw new ArgumentException("Product price must be greater than 0.");
            }
        }
    }
}