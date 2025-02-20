using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Backend.Models;
using Backend.Models.Domain;

namespace Backend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of categories.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of categories for the specified page.</returns>
        public async Task<IEnumerable<DomainCategory>> GetCategories(int pageNumber, int pageSize, string sortBy = "Name", string sortOrder = "asc", string filter = null)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(c => c.Name.Contains(filter) || c.Description.Contains(filter));
            }

            query = sortBy.ToLower() switch
            {
                "name" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "description" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Description) : query.OrderBy(c => c.Description),
                _ => query
            };

            var categories = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DomainCategory>>(categories);
        }

        /// <summary>
        /// Retrieves a paginated list of products for a category.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sortBy">The field to sort by (e.g., "Name").</param>
        /// <param name="sortOrder">The sort order ("asc" or "desc").</param>
        /// <param name="filter">The filter string to apply.</param>
        /// <returns>A list of products for the specified category and page.</returns>
        public async Task<IEnumerable<DomainProduct>> GetProducts(int categoryId, int pageNumber, int pageSize, string sortBy = "Name", string sortOrder = "asc", string filter = null)
        {
            var query = _context.Products.Where(p => p.Category.Id == categoryId).AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
            }

            query = sortBy.ToLower() switch
            {
                "name" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "price" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                _ => query
            };

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DomainProduct>>(products);
        }

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns>The added category.</returns>
        public async Task<DomainCategory> AddCategory(DomainCategory domainCategory)
        {
            var category = _mapper.Map<Category>(domainCategory);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<DomainCategory>(category);
        }

        /// <summary>
        /// Updates an existing category in the database.
        /// </summary>
        /// <param name="category">The category with updated information.</param>
        /// <returns>The updated category.</returns>
        public async Task<DomainCategory> UpdateCategory(DomainCategory domainCategory)
        {
            var category = _mapper.Map<Category>(domainCategory);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<DomainCategory>(category);
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}