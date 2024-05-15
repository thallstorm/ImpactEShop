using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto
{
	public class ProductDetailsResponseModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string? Brand { get; set; }
		public decimal? DiscountedPrice { get; set; }
		public int Stock { get; set; }
		public EShopStatusEnum Status { get; set; }
	}
}
