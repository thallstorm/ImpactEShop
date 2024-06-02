using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Implementations.Repositories
{
	public class BasketRepository : IBasketRepository	 //Defines class that implements the IBasketRepository interface.
	{													 //This repository is responsible for accessing and managing basket data.
		private readonly AppDbContext _dbcontext;

		public BasketRepository(AppDbContext dbcontext) //Constructor has a private readonly field named _dbcontext of type AppDbContext.
		{												//This injects a dependency on the application database context, responsible for interacting with the Database.
			_dbcontext = dbcontext;
		}

		public async Task<BasketDetailsResponseModel> GetBasketByCustomerId(Guid customerId)
		{
			var basket = await _dbcontext.Set<Basket>() //.Set<Basket>() tells the context to access the DbSet for the Basket entity type. This allows querying baskets from database.
				.Include(b => b.BasketItems)			//Include method for eager loading. Data retrieval in ORM frameworks. The query will also retrieve the associated basket items in a single db call.
				.ThenInclude(item => item.Product)		//For each retrieved BasketItem, the Product navigation property should also be included
				.FirstOrDefaultAsync(b => b.CustomerId == customerId); //Filters the retrieved baskets based on CustomerId parameter. Finds the first basket where the CustomerId matches and returns Basket Obj

			return basket?.Adapt<BasketDetailsResponseModel>(); //Checks if basket is not null and maps basket to dto.
		}

		public async Task<BasketDetailsResponseModel> AddItemToBasket(Guid customerId, Guid productId, int quantity)
		{
			var basket = await _dbcontext.Set<Basket>()
				.Include(b => b.BasketItems)
				.ThenInclude(item => item.Product)
				.FirstOrDefaultAsync(b => b.CustomerId == customerId);

			if (basket == null)
			{
				basket = new Basket { CustomerId = customerId };
				_dbcontext.Add(basket);
			}

			var product = await _dbcontext.Set<Product>().FindAsync(productId);
			if (product == null)
			{
				throw new ArgumentException("Invalid product Id");
			}

			var basketItem = basket.BasketItems.FirstOrDefault(item => item.ProductId == productId);
			if (basketItem == null)
			{
				basketItem = new BasketItem { ProductId = productId, Quantity = quantity, Product = product };
				basket.BasketItems.Add(basketItem);
			}
			else
			{
				basketItem.Quantity += quantity;
			}

			await _dbcontext.SaveChangesAsync();
			return basket.Adapt<BasketDetailsResponseModel>();
		}

		public async Task UpdateBasketItemQuantity(Guid customerId, Guid productId, int quantity)
		{
			var basket = await _dbcontext.Set<Basket>()
				.Include(b => b.BasketItems)
				.FirstOrDefaultAsync(b => b.CustomerId == customerId);

			if (basket == null)
			{
				throw new ArgumentException("Basket not found for customer");
			}

			var basketItem = basket.BasketItems.FirstOrDefault(item => item.ProductId == productId);
			if (basketItem == null)
			{
				throw new ArgumentException("Product not found in basket");
			}

			if (quantity <= 0)
			{
				basket.BasketItems.Remove(basketItem);
			}
			else
			{
				basketItem.Quantity = quantity;
			}

			await _dbcontext.SaveChangesAsync();
		}

		public async Task<bool> ClearBasketByCustomerId(Guid customerId)
		{
			var basket = await _dbcontext.Set<Basket>()
				.Include(b => b.BasketItems)
				.FirstOrDefaultAsync(b => b.CustomerId == customerId);

			if (basket == null)
			{
				return false;
			}

			_dbcontext.RemoveRange(basket.BasketItems);
			_dbcontext.Remove(basket);
			await _dbcontext.SaveChangesAsync();
			return true;
		}
	}
}
