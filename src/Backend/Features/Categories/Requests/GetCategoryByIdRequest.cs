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
