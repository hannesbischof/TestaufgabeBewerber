using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Name must not exceed 50 characters.")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The Description must not exceed 200 characters.")]
        public string Description { get; set; }
    }
}