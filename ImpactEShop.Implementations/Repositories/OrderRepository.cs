using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Implementations.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _dbContext;

		public OrderRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Order> CreateOrderAsync(Order order)
		{
			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();
			return order;
		}

		public async Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
		{
			return await _dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
		}

	}
}