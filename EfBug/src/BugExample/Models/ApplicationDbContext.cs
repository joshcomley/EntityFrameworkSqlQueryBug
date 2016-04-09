using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugExample.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder
				.Entity<Product>()
				.HasOne(u => u.CreatedByUser)
				.WithMany(u => u.ProductsCreated);
			builder
				.Entity<Product>()
				.HasOne(u => u.LastModifiedByUser)
				.WithMany(u => u.ProductsLastModified);
			builder
				.Entity<Order>()
				.HasOne(u => u.Customer)
				.WithMany(u => u.Orders);
			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			builder.UseSqlServer(
				"Server=.;Database=BugExample.App.Data;User ID=repulsingLogin;Password=5UmC*h8}po@:C>#3WP.j4|^p9;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
			base.OnConfiguring(builder);
		}
	}
}
