using ImpactEShop.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		//Tables
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Basket> Baskets { get; set; }
/*		public DbSet<EShopStatusEnum> EShopStatusEnum { get; set; }*/

	}
}
