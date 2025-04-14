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
        private readonly IManufacturerRepository _manufacturerRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IManufacturerRepository manufacturerRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<IEnumerable<DomainProduct>> GetProducts(int pageNumber, int pageSize, string sortBy = null, string sortOrder = null, string filter = null)
        {
            return await _productRepository.GetProducts(pageNumber, pageSize, sortBy, sortOrder, filter);
        }

        public async Task<DomainProduct> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<DomainProduct> AddProduct(DomainProduct product)
        {
            ValidateProduct(product);

            // Ensure the category exists.
            var category = await _categoryRepository.GetCategoryById(product.Category.Id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {product.Category.Id} does not exist.");
            }

            // Ensure the manufacturer exists.
            var manufacturer = await _manufacturerRepository.GetManufacturerById(product.Manufacturer.Id);
            if (manufacturer == null)
            {
                throw new ArgumentException($"Manufacturer with ID {product.Manufacturer.Id} does not exist.");
            }

            return await _productRepository.AddProduct(product);
        }

        public async Task<DomainProduct> UpdateProduct(DomainProduct product)
        {
            ValidateProduct(product);

            // Ensure the category exists.
            var category = await _categoryRepository.GetCategoryById(product.Category.Id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {product.Category.Id} does not exist.");
            }

            // Ensure the manufacturer exists.
            var manufacturer = await _manufacturerRepository.GetManufacturerById(product.Manufacturer.Id);
            if (manufacturer == null)
            {
                throw new ArgumentException($"Manufacturer with ID {product.Manufacturer.Id} does not exist.");
            }

            return await _productRepository.UpdateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }

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