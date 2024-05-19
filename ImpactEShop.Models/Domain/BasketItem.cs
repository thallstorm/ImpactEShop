using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain
{
	public class BasketItem
	{
		[Key]
		public Guid BasketId { get; set; } // Foreign key to Basket.CustomerId
		public Product Product { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price => Product.Price * Quantity;

	}
}
