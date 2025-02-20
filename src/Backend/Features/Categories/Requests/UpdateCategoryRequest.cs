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
```

### Step 4: Review the Code
- **Request Class**:
  - Implements `IRequest<DomainCategory>`.
  - Contains a constructor to initialize the `Category` property.
  - Adheres to the conventions used in the `GetCategoryByIdRequest.cs` file.

- **Request Handler**:
  - Implements `IRequestHandler<UpdateCategoryRequest, DomainCategory>`.
  - Uses `ICategoryService` to update the category.
  - Matches the structure and style of existing request handlers.

- **Dependencies**:
  - Uses `ICategoryService` for business logic.
  - Fully functional and adheres to the Mediator pattern.

### Final Output
The complete file content for `UpdateCategoryRequest.cs` is:

```
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
