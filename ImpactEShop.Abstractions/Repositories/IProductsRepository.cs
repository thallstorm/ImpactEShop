﻿using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories;
public interface IProductsRepository
{
	Task<Product> GetProductByIdAsync(Guid productId);
	Task<List<Product>> GetProductsAsync();
	Task CreateProductAsync(Product product);
	Task UpdateProductAsync(Product product);
	Task DeleteProductAsync(Guid productId);

	Task<int> GetTotalProductCountAsync(ProductFilter filter);
	Task<List<Product>> GetPageProductsAsync(int page, int pageSize, ProductFilter filter); // Add filter parameter
}