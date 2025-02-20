using Backend.Mediator;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Features.Products.Requests
{
    /// <summary>
    /// Mediator request for updating a product.
    /// </summary>
    public class UpdateProductRequest : IRequest<DomainProduct>
    {
        /// <summary>
        /// Gets the product to be updated.
        /// </summary>
        public DomainProduct Product { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductRequest"/> class.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public UpdateProductRequest(DomainProduct product)
        {
            Product = product;
        }
    }

    /// <summary>
    /// Handler for the <see cref="UpdateProductRequest"/>.
    /// </summary>
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, DomainProduct>
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductRequestHandler"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public UpdateProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        /// <inheritdoc />
        public async Task<DomainProduct> Handle(UpdateProductRequest request)
        {
            return await _productService.UpdateProduct(request.Product);
        }
    }
}
