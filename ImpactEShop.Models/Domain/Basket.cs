using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain;
public sealed class Basket
    
{
	[Key]
	public Guid CustomerId { get; set; }
	public List<BasketItem> BasketItems { get; set; } = new();
	public decimal Price => BasketItems.Sum(item => item.Price);
}
