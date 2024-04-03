using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories;
public interface IProductsRepository
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> GetProductsAsync(CancellationToken cancellationToken);
    Task<Product> CreateProductAsync(ModifyProductInfo productInfo, CancellationToken cancellationToken);
    Task<Product> UpdateProductAsync(Guid id, ModifyProductInfo productInfo, CancellationToken cancellationToken);
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
}

public record ModifyProductInfo
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public string? Brand { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public required int Stock { get; set; }
    public required EShopStatusEnum Status { get; set; }
}