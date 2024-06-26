﻿using ImpactEShop.Models.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain
{
	public sealed class Order
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid(); // Generate new GUID on creation
		public Guid CustomerId { get; set; } // Reference to the customer
		public List<OrderItem> OrderItems { get; set; } = new();
		public decimal Price { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Set order date to current UTC time
	}

	public class OrderItem
	{
		[Key]
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}

}