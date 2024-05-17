using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories;
public sealed class ProductsRepository : IProductsRepository
{
	private readonly AppDbContext _dbContext;

	public ProductsRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}


	public async Task<Product> GetProductByIdAsync(Guid id)
    {
		return await _dbContext.Products.FindAsync(id);
	}

	public async Task<List<Product>> GetProductsAsync()
	{
		return await _dbContext.Products.ToListAsync();
	}

	private IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Brand))
        {
            query = query.Where(p => p.Brand == filter.Brand);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        return query;
    }
	public async Task<int> GetTotalProductCountAsync(ProductFilter filter)
	{
		var query = _dbContext.Products.AsQueryable();
		query = ApplyFilters(query, filter);

		return await query.CountAsync();
	}

	public async Task<List<Product>> GetPageProductsAsync(int page, int pageSize, ProductFilter filter)
	{
		var query = _dbContext.Products.AsQueryable();
		query = ApplyFilters(query, filter);

		return await query.Skip((page - 1) * pageSize)
						  .Take(pageSize)
						  .ToListAsync();
	}

	public async Task CreateProductAsync(Product product)
	{
		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateProductAsync(Product product)
	{
		_dbContext.Products.Update(product);
		await _dbContext.SaveChangesAsync();
	}

	/*public async Task DeleteProductAsync(Guid id)
	{
		var productToDelete = await _dbContext.Products.FindAsync(id);
		if (productToDelete != null)
		{
			_dbContext.Products.Remove(productToDelete);
			await _dbContext.SaveChangesAsync();
		}
	}*/

	public async Task DeleteProductAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			throw new ArgumentException("Invalid product id");
		}

		var productToDelete = await _dbContext.Products.FindAsync(id);
		if (productToDelete != null)
		{
			_dbContext.Products.Remove(productToDelete);
			await _dbContext.SaveChangesAsync();
		}
	}
}
