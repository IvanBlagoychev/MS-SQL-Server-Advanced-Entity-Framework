namespace _2.Local_Store_Improvement
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductsNewContext : DbContext
    {
        
        public ProductsNewContext()
            : base("name=ProductsNewContext")
        {
        }
        
        public virtual DbSet<Product> ProductsImproved { get; set; }
    }
}