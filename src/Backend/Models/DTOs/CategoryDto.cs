using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for Category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "The Name must not exceed 50 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "The Description must not exceed 200 characters.")]
        public string Description { get; set; }
    }
}
