using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for Product.
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The Name must be between 5 and 100 characters.")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "The Description must be between 10 and 500 characters.")]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // New: ManufacturerId field
        [Required]
        public int ManufacturerId { get; set; }
    }
}