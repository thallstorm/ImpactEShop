using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain;
public sealed class Order
{
	public required Guid Id { get; set; }
	public required Guid CustomerId { get; set; } 
	public required Customer Customer { get; set; } 
	public List<Product> Products { get; set; } = new();
}
