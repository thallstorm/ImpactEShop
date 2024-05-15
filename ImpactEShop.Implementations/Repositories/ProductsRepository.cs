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
		/*throw new NotImplementedException();*/
		return await _dbContext.Products.FindAsync(id);
	}

	public async Task<List<Product>> GetProductsAsync()
	{
		return await _dbContext.Products.ToListAsync();
	}

	/*public async Task<List<Product>> GetProductsAsync(int page, int pageSize,
		string brandFilter = null, decimal? minPrice = null, decimal? maxPrice = null)
	{
		var query = _dbContext.Products.AsQueryable();

		if (!string.IsNullOrEmpty(brandFilter))
		{
			query = query.Where(p => p.Brand.Contains(brandFilter));
		}

		if (minPrice.HasValue)
		{
			query = query.Where(p => p.Price >= minPrice.Value);
		}

		if (maxPrice.HasValue)
		{
			query = query.Where(p => p.Price <= maxPrice.Value);
		}

		return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
	}*/

	public async Task GetProductsAsync(int page, int pageSize,
		string brandFilter = null, decimal? minPrice = null, decimal? maxPrice = null)
	{
		var query = _dbContext.Products.AsQueryable();

		if (!string.IsNullOrEmpty(brandFilter))
		{
			query = query.Where(p => p.Brand.Contains(brandFilter));
		}

		if (minPrice.HasValue)
		{
			query = query.Where(p => p.Price >= minPrice.Value);
		}

		if (maxPrice.HasValue)
		{
			query = query.Where(p => p.Price <= maxPrice.Value);
		}

		await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
	}

	public async Task<Product> CreateProductAsync(Product product)
	{
		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync();
		return product;
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

	public async Task<int> GetTotalProductCountAsync(ProductFilter filter)
	{
		var query = _dbContext.Products.AsQueryable();

		if (!string.IsNullOrEmpty(filter.Brand))
		{
			query = query.Where(p => p.Brand.Contains(filter.Brand));
		}

		if (filter.MinPrice.HasValue)
		{
			query = query.Where(p => p.Price >= filter.MinPrice.Value);
		}

		if (filter.MaxPrice.HasValue)
		{
			query = query.Where(p => p.Price <= filter.MaxPrice.Value);
		}

		return await query.CountAsync();
	}
}
