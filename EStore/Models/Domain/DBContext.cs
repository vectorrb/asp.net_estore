using EStore.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EStore.Models.Domain
{
	public class DBContext : IdentityDbContext<ApplicationUser>
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{
			
		}

		public DbSet<Genre> Genres { get; set; }
		public DbSet<MovieGenre> MovieGenres { get; set; }
		public DbSet<Movie> Movies { get; set; }

		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<CartDetail> CartDetails { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		public DbSet<OrderStatus> orderStatuses { get; set; }

	}
}
