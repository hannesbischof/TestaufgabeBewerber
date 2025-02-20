using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    /// <summary>
    /// Mediator request for updating a category.
    /// </summary>
    public class UpdateCategoryRequest : IRequest<DomainCategory>
    {
        /// <summary>
        /// Gets the category to be updated.
        /// </summary>
        public DomainCategory Category { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryRequest"/> class.
        /// </summary>
        /// <param name="category">The category to update.</param>
        public UpdateCategoryRequest(DomainCategory category)
        {
            Category = category;
        }
    }

    /// <summary>
    /// Handler for the <see cref="UpdateCategoryRequest"/>.
    /// </summary>
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, DomainCategory>
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public UpdateCategoryRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <inheritdoc />
        public async Task<DomainCategory> Handle(UpdateCategoryRequest request)
        {
            return await _categoryService.UpdateCategory(request.Category);
        }
    }
}
