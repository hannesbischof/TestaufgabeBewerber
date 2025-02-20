using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    /// <summary>
    /// Mediator request for retrieving a category by its ID.
    /// </summary>
    public class GetCategoryByIdRequest : IRequest<DomainCategory?>
    {
        /// <summary>
        /// Gets or sets the ID of the category to retrieve.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        public GetCategoryByIdRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="GetCategoryByIdRequest"/>.
    /// </summary>
    public class GetCategoryByIdRequestHandler : IRequestHandler<GetCategoryByIdRequest, DomainCategory?>
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdRequestHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public GetCategoryByIdRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <inheritdoc />
        public async Task<DomainCategory?> Handle(GetCategoryByIdRequest request)
        {
            return await _categoryService.GetCategoryById(request.Id);
        }
    }
}
```

### Step 4: Review the Code
- **Request Class**:
  - Implements `IRequest<DomainCategory?>`.
  - Contains a constructor to initialize the `Id` property.
- **Request Handler**:
  - Implements `IRequestHandler<GetCategoryByIdRequest, DomainCategory?>`.
  - Uses `ICategoryService` to fetch the category by ID.
- **Conventions**:
  - Matches the structure and style of existing request files in the codebase.
  - Fully functional and adheres to the Mediator pattern.

### Final Output
The complete file content for `GetCategoryByIdRequest.cs` is:

```
using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    /// <summary>
    /// Mediator request for retrieving a category by its ID.
    /// </summary>
    public class GetCategoryByIdRequest : IRequest<DomainCategory?>
    {
        /// <summary>
        /// Gets or sets the ID of the category to retrieve.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        public GetCategoryByIdRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="GetCategoryByIdRequest"/>.
    /// </summary>
    public class GetCategoryByIdRequestHandler : IRequestHandler<GetCategoryByIdRequest, DomainCategory?>
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdRequestHandler"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public GetCategoryByIdRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <inheritdoc />
        public async Task<DomainCategory?> Handle(GetCategoryByIdRequest request)
        {
            return await _categoryService.GetCategoryById(request.Id);
        }
    }
}
