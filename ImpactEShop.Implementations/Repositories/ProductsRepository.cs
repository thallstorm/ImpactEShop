using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories;
public sealed class ProductsRepository : IProductsRepository
{
    public Task<Product> CreateProductAsync(ModifyProductInfo productInfo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateProductAsync(Guid id, ModifyProductInfo productInfo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
