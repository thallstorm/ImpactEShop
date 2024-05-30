using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories
{
	public interface IBasketRepository
	{
		Task<BasketDetailsResponseModel> GetBasketByCustomerId(Guid customerId);
		Task<BasketDetailsResponseModel> AddItemToBasket(Guid customerId, Guid productId, int quantity);
		Task UpdateBasketItemQuantity(Guid customerId, Guid productId, int quantity);
		Task<bool> ClearBasketByCustomerId(Guid customerId);
	}
}
