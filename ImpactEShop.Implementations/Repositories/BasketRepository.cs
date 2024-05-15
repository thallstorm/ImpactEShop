using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Implementations.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDictionary<Guid, List<BasketItem>> _baskets;
		private readonly IProductsRepository _productsRepository;

		public BasketRepository(IDictionary<Guid, List<BasketItem>> baskets, IProductsRepository productsRepository)
		{
			_baskets = baskets;
			_productsRepository = productsRepository;
		}

		public async Task<List<BasketItem>> GetBasketAsync(Guid customerId)
		{
			if (!_baskets.ContainsKey(customerId))
			{
				_baskets.TryAdd(customerId, new List<BasketItem>());
			}

			return _baskets[customerId];
		}

		public async Task AddProductToBasketAsync(Guid customerId, Guid productId, int quantity)
		{
			var basket = await GetBasketAsync(customerId);

			// Retrieve product details using dependency injected IProductsRepository
			var product = await _productsRepository.GetProductByIdAsync(productId);

			if (product != null && CanAddToBasket(product))
			{
				var existingItem = basket.FirstOrDefault(item => item.Product.Id == productId);
				if (existingItem != null)
				{
					existingItem.Quantity += quantity;
				}
				else
				{
					basket.Add(new BasketItem { Product = product, Quantity = quantity });
				}
			}
		}

		public async Task RemoveProductFromBasketAsync(Guid customerId, Guid productId)
		{
			var basket = await GetBasketAsync(customerId);
			var itemToRemove = basket.FirstOrDefault(item => item.Product.Id == productId);
			if (itemToRemove != null)
			{
				basket.Remove(itemToRemove);
			}
		}

		public async Task UpdateProductQuantityInBasketAsync(Guid customerId, Guid productId, int newQuantity)
		{
			var basket = await GetBasketAsync(customerId);
			var itemToUpdate = basket.FirstOrDefault(item => item.Product.Id == productId);
			if (itemToUpdate != null)
			{
				itemToUpdate.Quantity = newQuantity;
			}
		}

		private bool CanAddToBasket(Product product)
		{
			// Implement logic based on product availability rules
			return product.Stock > 0 && (
				(product.Status == EShopStatusEnum.One && product.Stock > 0) || // Status 1, any stock
				(product.Status == EShopStatusEnum.Two && product.Stock > 2) || // Status 2, stock > 2
				product.Status == EShopStatusEnum.Three // Status 3, regardless of stock
			);
		}
	}
}
