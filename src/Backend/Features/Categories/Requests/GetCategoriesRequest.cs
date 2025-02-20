using Backend.Mediator;
using Backend.Models.Domain;

namespace Backend.Features.Categories.Requests
{
    public class GetCategoriesRequest : IRequest<IEnumerable<DomainCategory>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetCategoriesRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<DomainCategory>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoriesRequestHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<DomainCategory>> Handle(GetCategoriesRequest request)
        {
            return await _categoryService.GetCategories(request.PageNumber, request.PageSize);
        }
    }
}
