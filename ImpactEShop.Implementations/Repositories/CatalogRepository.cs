using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Implementations.Repositories
{
	public class CatalogRepository : IProductsRepository
	{
		private readonly AppDbContext _dbContext;

		public CatalogRepository(AppDbContext dbContext)
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

		Task<Product> IProductsRepository.CreateProductAsync(Product product)
		{
			throw new NotImplementedException();
		}

		public Task<int> GetTotalProductCountAsync(ProductFilter filter)
		{
			throw new NotImplementedException();
		}
	}
}
