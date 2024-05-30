using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto
{
	public class BasketDetailsResponseModel
	{
		public Guid CustomerId { get; set; }
		public decimal TotalPrice { get; set; }
		public List<BasketItemDetails> BasketItems { get; set; } = new();

		public class BasketItemDetails
		{
			public Guid ProductId { get; set; }
			public string ProductName { get; set; }
			public int Quantity { get; set; }
			public decimal Price { get; set; }
		}
	}
}
