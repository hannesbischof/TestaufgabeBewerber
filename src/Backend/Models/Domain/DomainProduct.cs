namespace Backend.Models.Domain
{
    /// <summary>
    /// Domain model for Product.
    /// </summary>
    public class DomainProduct
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category associated with the product.
        /// </summary>
        public DomainCategory Category { get; set; }
    }
}
```

---

### Step 4: Review the Code
- The file is named `DomainProduct.cs` and is located in the `src/Backend/Models/Domain/` directory.
- The `DomainProduct` class is defined within the `Backend.Models.Domain` namespace.
- The class includes all the required properties (`Id`, `Name`, `Price`, `Description`, and `Category`).
- The `Category` property is of type `DomainCategory`, as specified in the instructions.
- The code follows the conventions and style used in the codebase, as seen in the `DomainCategory` class.

---

### Final Output
```
namespace Backend.Models.Domain
{
    /// <summary>
    /// Domain model for Product.
    /// </summary>
    public class DomainProduct
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category associated with the product.
        /// </summary>
        public DomainCategory Category { get; set; }
    }
}
