namespace _1.Local_Store
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GickaContext : DbContext
    {
        public GickaContext()
            : base("name=GickaContext")
        {
        }       

        public virtual DbSet<Product> Products { get; set; }
    }
}