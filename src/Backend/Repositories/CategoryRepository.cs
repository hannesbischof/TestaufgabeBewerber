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
        public async Task<IEnumerable<DomainCategory>> GetCategories(int pageNumber, int pageSize)
        {
            var categories = await _context.Categories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DomainCategory>>(categories);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID, or null if not found.</returns>
        public async Task<DomainCategory> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.Map<DomainCategory>(category);
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