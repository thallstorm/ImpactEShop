using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain;
public sealed class Product
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public string? Brand { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public required int Stock { get; set; }
    public required EShopStatusEnum Status { get; set; }
}
