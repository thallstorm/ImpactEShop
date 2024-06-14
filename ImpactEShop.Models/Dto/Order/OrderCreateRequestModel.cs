using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto.Order
{
	public class OrderCreateRequestModel
	{
		[Key]
		public Guid CustomerId { get; set; } // Reference to the customer
		public decimal TotalPrice { get; set; }
		public List<OrderItemRequestModel> OrderItems { get; set; } = new();

	}

	public class OrderItemRequestModel
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; } // Assuming price is retrieved from another source during order creation
	}
}
