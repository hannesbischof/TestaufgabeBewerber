using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Products.Requests
{
    /// <summary>
    /// Mediator request for retrieving a product by its ID.
    /// </summary>
    public class GetProductByIdRequest : IRequest<DomainProduct?>
    {
        /// <summary>
        /// Gets the ID of the product to retrieve.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        public GetProductByIdRequest(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Handler for the <see cref="GetProductByIdRequest"/>.
    /// </summary>
    public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdRequest, DomainProduct?>
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdRequestHandler"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public GetProductByIdRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        /// <inheritdoc />
        public async Task<DomainProduct?> Handle(GetProductByIdRequest request)
        {
            return await _productService.GetProductById(request.Id);
        }
    }
}
