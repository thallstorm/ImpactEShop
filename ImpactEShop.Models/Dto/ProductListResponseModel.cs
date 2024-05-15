using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto
{
	public class ProductListResponseModel
	{
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalProducts { get; set; }
		public List<ProductSummaryResponseModel> Products { get; set; }
	}
}
