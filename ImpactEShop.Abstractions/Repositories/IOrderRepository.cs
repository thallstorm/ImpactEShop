using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories
{
	public interface IOrderRepository
	{
		Task<Order> CreateOrderAsync(Order order);
		Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId);

	}
}