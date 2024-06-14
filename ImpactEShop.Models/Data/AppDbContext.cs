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
	public class AppDbContext : DbContext //inherits from DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //The Constructor takes a parameter of type DbContextOptions<AppDbContext>, which contains config options for db connection
		{																			//the base Contructor is called, passing the received options to the base class for initialization
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) //This method is inherited from DbContext. It's a virtual method that allows config the EF Core model to the app.
		{																   //In this case no explicit model config is applied in this method.
		}																   //Often used to define mappings between app's classes like Product and db tables, specify data annotations (column constraints)
																		   //And configure relationshipss between entities
		//Tables
		public DbSet<Product> Products { get; set; }            //DbSet<Product> is a generic type that represents a collection of Product entities, mapped to the Products table in the database
																//This Products Property provides access to the Products table data through methods like Add, Find, allowing CRUD operations on Product data.
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Basket> Baskets { get; set; }
/*		public DbSet<EShopStatusEnum> EShopStatusEnum { get; set; }*/

	}
}
