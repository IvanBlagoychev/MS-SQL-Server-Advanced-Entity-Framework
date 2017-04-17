namespace Tasks1to3.Data
{
    using Tasks1to3;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("name=ProductsContext")
        {
            Database.SetInitializer<ProductsContext>(new DropCreateDatabaseIfModelChanges<ProductsContext>());
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(f => f.ProductsBought)
                .WithOptional(p => p.Buyer);

            modelBuilder.Entity<User>()
                .HasMany(f => f.ProductsSold)
                .WithRequired(p => p.Seller);

            modelBuilder.Entity<User>().HasMany(u=>u.Friends).WithMany();

            modelBuilder.Entity<User>()
                .HasMany(f => f.Friends)
                .WithMany()
                .Map(t =>
                {
                    t.MapLeftKey("UserId");
                    t.MapRightKey("FriendId");
                    t.ToTable("UserFriends");
                });

            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}