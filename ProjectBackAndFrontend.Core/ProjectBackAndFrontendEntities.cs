using ProjectBackAndFrontend.Core.Models;
using System.Data.Entity;

namespace ProjectBackAndFrontend.Core
{
    public class ProjectBackAndFrontendEntities : DbContext
    {

        public ProjectBackAndFrontendEntities() : base("name=ProjectBackAndFrontendEntities")
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<Offer> Offer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Offer)
                .WithRequired(o => o.Product)
                .HasForeignKey(o => o.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Order)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Offer)
                .WithMany(e => e.Order)
                .Map(m => m.ToTable("OrderItems", "Order").MapLeftKey("OrderId").MapRightKey("OfferId"));
        }
    }
}