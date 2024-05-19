using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using Microsoft.EntityFrameworkCore;
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
    private readonly AppDbContext _dbcontext;

    public BasketRepository(AppDbContext dbcontext)
    {
			_dbcontext = dbcontext;
    }

    public async Task<Basket> GetBasketByCustomerId(Guid customerId)
    {
        var basket = await _dbcontext.Set<Basket>()
            .Include(b => b.BasketItems)
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(b => b.CustomerId == customerId);

        return basket;
    }

    public async Task<Basket> AddItemToBasket(Guid customerId, Guid productId, int quantity)
    {
        var basket = await GetBasketByCustomerId(customerId);
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
        return basket;
    }

    public async Task UpdateBasketItemQuantity(Guid customerId, Guid productId, int quantity)
    {
        var basket = await GetBasketByCustomerId(customerId);
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
        var basket = await GetBasketByCustomerId(customerId);
        if (basket == null)
        {
            return false;
        }

        _dbcontext.BasketItems.RemoveRange(basket.BasketItems);
        _dbcontext.Baskets.Remove(basket);
        await _dbcontext.SaveChangesAsync();
        return true;
    }
}
}
