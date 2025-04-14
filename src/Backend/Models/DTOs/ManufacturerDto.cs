using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object for Manufacturer.
    /// </summary>
    public class ManufacturerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Name must not exceed 100 characters.")]
        public string Name { get; set; }
    }
}