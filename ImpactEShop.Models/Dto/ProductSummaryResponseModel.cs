using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto
{
	public class ProductSummaryResponseModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string? Brand { get; set; }
		public string? ImageUrl { get; set; } // Optional property for product image URL
	}
}
