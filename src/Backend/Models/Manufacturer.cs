using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Name must not exceed 100 characters.")]
        public string Name { get; set; }

        // Navigation property for associated products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}