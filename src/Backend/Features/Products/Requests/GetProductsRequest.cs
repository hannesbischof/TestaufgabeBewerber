using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Products.Requests
{
    public class GetProductsRequest : IRequest<IEnumerable<DomainProduct>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetProductsRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, IEnumerable<DomainProduct>>
    {
        private readonly IProductService _productService;

        public GetProductsRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<DomainProduct>> Handle(GetProductsRequest request)
        {
            return await _productService.GetProducts(request.PageNumber, request.PageSize);
        }
    }
}
