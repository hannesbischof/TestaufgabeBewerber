using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Manufacturers.Requests
{
    public class GetManufacturersRequest : IRequest<IEnumerable<DomainManufacturer>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public string? Filter { get; set; }

        public GetManufacturersRequest(int pageNumber, int pageSize, string? sortBy = null, string? sortOrder = null, string? filter = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortBy = sortBy;
            SortOrder = sortOrder;
            Filter = filter;
        }
    }

    public class GetManufacturersRequestHandler : IRequestHandler<GetManufacturersRequest, IEnumerable<DomainManufacturer>>
    {
        private readonly IManufacturerService _manufacturerService;

        public GetManufacturersRequestHandler(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public async Task<IEnumerable<DomainManufacturer>> Handle(GetManufacturersRequest request)
        {
            return await _manufacturerService.GetManufacturers(request.PageNumber, request.PageSize, request.SortBy, request.SortOrder, request.Filter);
        }
    }
}