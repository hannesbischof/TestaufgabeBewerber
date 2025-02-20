using Backend.Mediator;
using Backend.Services;

namespace Backend.Features.Products.Requests
{
    /// <summary>
    /// Mediator request for deleting a product by its ID.
    /// </summary>
    public class DeleteProductRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets the ID of the product to delete.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public DeleteProductRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="DeleteProductRequest"/>.
    /// </summary>
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Unit>
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductRequestHandler"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public DeleteProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(DeleteProductRequest request)
        {
            await _productService.DeleteProduct(request.Id);
            return Unit.Value;
        }
    }
}
```

### Step 4: Review the Code
- **Request Class**:
  - Implements `IRequest<Unit>`.
  - Contains a constructor to initialize the `Id` property.
  - Matches the conventions used in similar files (e.g., `DeleteCategoryRequest.cs`).

- **Request Handler**:
  - Implements `IRequestHandler<DeleteProductRequest, Unit>`.
  - Uses `IProductService` to delete the product by its ID.
  - Returns `Unit.Value` to indicate successful completion.

- **Conventions**:
  - The file adheres to the structure and style of existing request files in the codebase.
  - Fully functional and follows the Mediator pattern.

### Final Output
The complete file content for `DeleteProductRequest.cs` is:

```
using Backend.Mediator;
using Backend.Services;

namespace Backend.Features.Products.Requests
{
    /// <summary>
    /// Mediator request for deleting a product by its ID.
    /// </summary>
    public class DeleteProductRequest : IRequest<Unit>
    {
        /// <summary>
        /// Gets the ID of the product to delete.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public DeleteProductRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="DeleteProductRequest"/>.
    /// </summary>
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Unit>
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductRequestHandler"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public DeleteProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(DeleteProductRequest request)
        {
            await _productService.DeleteProduct(request.Id);
            return Unit.Value;
        }
    }
}
