using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Backend.Models;
using Backend.Models.Domain;

namespace Backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of products for the specified page.</returns>
        public async Task<IEnumerable<DomainProduct>> GetProducts(int pageNumber, int pageSize, string sortBy = null, string sortOrder = "asc", string filter = null)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortOrder.ToLower() == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                    : query.OrderBy(e => EF.Property<object>(e, sortBy));
            }

            // Apply pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var products = await query.ToListAsync();
            return _mapper.Map<IEnumerable<DomainProduct>>(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID, or null if not found.</returns>
        public async Task<DomainProduct> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<DomainProduct>(product);
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public async Task<DomainProduct> AddProduct(DomainProduct domainProduct)
        {
            var product = _mapper.Map<Product>(domainProduct);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<DomainProduct>(product);
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product with updated information.</param>
        /// <returns>The updated product.</returns>
        public async Task<DomainProduct> UpdateProduct(DomainProduct domainProduct)
        {
            var product = _mapper.Map<Product>(domainProduct);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<DomainProduct>(product);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}