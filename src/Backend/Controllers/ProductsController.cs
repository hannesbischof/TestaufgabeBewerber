using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Backend.Models.DTOs;
using Backend.Models.Domain;
using Backend.Services;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller for managing product-related API endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of products for the specified page.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var request = new GetProductsRequest(pageNumber, pageSize);
            var products = await _mediator.Send(request);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID, or a 404 status if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDto">The product to add.</param>
        /// <returns>The added product.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var domainProduct = _mapper.Map<DomainProduct>(productDto);
                var createdProduct = await _productService.AddProduct(domainProduct);
                var createdProductDto = _mapper.Map<ProductDto>(createdProduct);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.Id }, createdProductDto);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">The updated product information.</param>
        /// <returns>The updated product, or a 404 status if not found.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest(new { message = "Product ID mismatch." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var domainProduct = _mapper.Map<DomainProduct>(productDto);
                var updatedProduct = await _productService.UpdateProduct(domainProduct);
                var updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);
                return Ok(updatedProductDto);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A 204 status if the product was deleted, or a 404 status if not found.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}