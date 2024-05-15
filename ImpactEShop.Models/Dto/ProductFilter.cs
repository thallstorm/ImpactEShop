using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto
{
	public class ProductFilter
	{
		public string Brand { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
	}
}
