using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Products.Requests
{
    /// <summary>
    /// Mediator request for adding a new product.
    /// </summary>
    public class AddProductRequest : IRequest<DomainProduct>
    {
        /// <summary>
        /// Gets the product to be added.
        /// </summary>
        public DomainProduct Product { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductRequest"/> class.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public AddProductRequest(DomainProduct product)
        {
            Product = product;
        }
    }

    /// <summary>
    /// Handler for the <see cref="AddProductRequest"/>.
    /// </summary>
    public class AddProductRequestHandler : IRequestHandler<AddProductRequest, DomainProduct>
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductRequestHandler"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public AddProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        /// <inheritdoc />
        public async Task<DomainProduct> Handle(AddProductRequest request)
        {
            return await _productService.AddProduct(request.Product);
        }
    }
}
