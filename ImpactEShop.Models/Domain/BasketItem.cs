using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain
{
	public class BasketItem
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
