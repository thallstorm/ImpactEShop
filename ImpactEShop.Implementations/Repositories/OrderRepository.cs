using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto.Order;
using Mapster;
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

		public async Task<OrderDetailsResponseModel> SubmitOrder(OrderCreateRequestModel orderRequest)
		{
			var order = orderRequest.Adapt<Order>();
			order.Id = Guid.NewGuid();
			order.OrderDate = DateTime.UtcNow;

			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();

			return order.Adapt<OrderDetailsResponseModel>();
		}

		public async Task<OrderDetailsResponseModel> GetOrderById(Guid orderId)
		{
			var order = await _dbContext.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.Id == orderId);

			if (order == null)
			{
				return null;
			}

			return order.Adapt<OrderDetailsResponseModel>();
		}
		public async Task<bool> DeleteOrder(Guid orderId)
		{
			var order = await _dbContext.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.Id == orderId);

			if (order == null)
			{
				return false;
			}

			_dbContext.OrderItems.RemoveRange(order.OrderItems);
			_dbContext.Orders.Remove(order);
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}
}