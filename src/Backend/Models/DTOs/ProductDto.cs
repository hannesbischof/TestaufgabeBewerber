using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for Product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The Name must be between 5 and 100 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        [Required]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "The Description must be between 10 and 500 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category ID associated with the product.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
    }
}
