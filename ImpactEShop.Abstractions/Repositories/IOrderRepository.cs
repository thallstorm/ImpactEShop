using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories
{
	public interface IOrderRepository
	{
		Task<OrderDetailsResponseModel> SubmitOrder(OrderCreateRequestModel orderRequest);
		Task<OrderDetailsResponseModel> GetOrderById(Guid orderId);
		Task<bool> DeleteOrder(Guid orderId);
	}
}