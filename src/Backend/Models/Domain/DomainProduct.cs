namespace Backend.Models.Domain
{
    /// <summary>
    /// Domain model for Product.
    /// </summary>
    public class DomainProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DomainCategory Category { get; set; }
        public DomainManufacturer Manufacturer { get; set; }
    }
}