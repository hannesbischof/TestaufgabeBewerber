using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Categories.Requests
{
    public class GetCategoriesRequest : IRequest<IEnumerable<DomainCategory>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public string? Filter { get; set; }

        public GetCategoriesRequest(int pageNumber, int pageSize, string? sortBy = null, string? sortOrder = null, string? filter = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortBy = sortBy;
            SortOrder = sortOrder;
            Filter = filter;
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
            return await _categoryService.GetCategories(request.PageNumber, request.PageSize, request.SortBy, request.SortOrder, request.Filter);
        }
    }
}