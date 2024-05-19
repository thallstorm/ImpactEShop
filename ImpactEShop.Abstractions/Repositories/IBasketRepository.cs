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
		Task<Basket> GetBasketByCustomerId(Guid customerId);
		Task<Basket> AddItemToBasket(Guid customerId, Guid productId, int quantity);
		Task UpdateBasketItemQuantity(Guid customerId, Guid productId, int quantity);
		Task<bool> ClearBasketByCustomerId(Guid customerId);
	}
}
