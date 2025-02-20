using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    /// <summary>
    /// Mediator request for adding a new category.
    /// </summary>
    public class AddCategoryRequest : IRequest<DomainCategory>
    {
        /// <summary>
        /// Gets the category to be added.
        /// </summary>
        public DomainCategory Category { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCategoryRequest"/> class.
        /// </summary>
        /// <param name="category">The category to add.</param>
        public AddCategoryRequest(DomainCategory category)
        {
            Category = category;
        }
    }

    /// <summary>
    /// Handler for the <see cref="AddCategoryRequest"/>.
    /// </summary>
    public class AddCategoryRequestHandler : IRequestHandler<AddCategoryRequest, DomainCategory>
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public AddCategoryRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <inheritdoc />
        public async Task<DomainCategory> Handle(AddCategoryRequest request)
        {
            return await _categoryService.AddCategory(request.Category);
        }
    }
}
