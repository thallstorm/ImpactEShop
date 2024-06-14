using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto.Order
{
	public class OrderDetailsResponseModel
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid(); // Generate new GUID on creation
		public Guid CustomerId { get; set; } // Reference to the customer
		public List<OrderItemResponseModel> OrderItems { get; set; } = new();
		public decimal TotalPrice { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Set order date to current UTC time
	}

	public class OrderItemResponseModel
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; } // Assuming price is retrieved from another source during order creation
	}
}
