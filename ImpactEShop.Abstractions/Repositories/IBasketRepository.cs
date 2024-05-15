using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories
{
	public interface IBasketRepository
	{
		Task<List<BasketItem>> GetBasketAsync(Guid customerId);
		Task AddProductToBasketAsync(Guid customerId, Guid productId, int quantity);
		Task RemoveProductFromBasketAsync(Guid customerId, Guid productId);
		Task UpdateProductQuantityInBasketAsync(Guid customerId, Guid productId, int newQuantity);
	}
}
