using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Backend.Models.DTOs;
using Backend.Mediator;
using Backend.Features.Categories.Requests;
using Backend.Models.Domain;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller for managing category-related API endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator service.</param>
        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of categories.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of categories for the specified page.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(int pageNumber = 1, int pageSize = 10, string sortBy = null, string sortOrder = null, string filter = null)
        {
            var request = new GetCategoriesRequest(pageNumber, pageSize, sortBy, sortOrder, filter);
            var categories = await _mediator.Send(request);
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID, or a 404 status if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdRequest(id));
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns>The added category.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var domainCategory = _mapper.Map<DomainCategory>(categoryDto);
                var createdCategory = await _mediator.Send(new AddCategoryRequest(domainCategory));
                var createdCategoryDto = _mapper.Map<CategoryDto>(createdCategory);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategoryDto.Id }, createdCategoryDto);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="category">The updated category information.</param>
        /// <returns>The updated category, or a 404 status if not found.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest(new { message = "Category ID mismatch." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var domainCategory = _mapper.Map<DomainCategory>(categoryDto);
                var updatedCategory = await _mediator.Send(new UpdateCategoryRequest(domainCategory));
                var updatedCategoryDto = _mapper.Map<CategoryDto>(updatedCategory);
                return Ok(updatedCategoryDto);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A 204 status if the category was deleted, or a 404 status if not found.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdRequest(id));
            if (category == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteCategoryRequest(id));
            return NoContent();
        }
    }
}