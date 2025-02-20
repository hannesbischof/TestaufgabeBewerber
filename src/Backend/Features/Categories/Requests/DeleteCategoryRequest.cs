using Backend.Mediator;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    /// <summary>
    /// Mediator request for deleting a category by its ID.
    /// </summary>
    public class DeleteCategoryRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets the ID of the category to delete.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        public DeleteCategoryRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="DeleteCategoryRequest"/>.
    /// </summary>
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Unit>
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public DeleteCategoryRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(DeleteCategoryRequest request)
        {
            await _categoryService.DeleteCategory(request.Id);
            return Unit.Value;
        }
    }
}
```

### Step 4: Review the Code
- **Request Class**:
  - Implements `IRequest<Unit>`.
  - Contains a constructor to initialize the `Id` property.
  - Matches the conventions used in other request files.

- **Request Handler**:
  - Implements `IRequestHandler<DeleteCategoryRequest, Unit>`.
  - Uses `ICategoryService` to delete the category by its ID.
  - Returns `Unit.Value` to indicate successful completion.

- **Dependencies**:
  - Uses `ICategoryService` for business logic.
  - Fully functional and adheres to the Mediator pattern.

### Final Output
The complete file content for `DeleteCategoryRequest.cs` is:

```
using Backend.Mediator;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    /// <summary>
    /// Mediator request for deleting a category by its ID.
    /// </summary>
    public class DeleteCategoryRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets the ID of the category to delete.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        public DeleteCategoryRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="DeleteCategoryRequest"/>.
    /// </summary>
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Unit>
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public DeleteCategoryRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(DeleteCategoryRequest request)
        {
            await _categoryService.DeleteCategory(request.Id);
            return Unit.Value;
        }
    }
}
