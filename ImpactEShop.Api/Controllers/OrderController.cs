using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Implementations.Repositories;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto.Order;
using ImpactEShop.Models.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ImpactEShop.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IBasketRepository _basketRepository;

		public OrderController(IOrderRepository orderRepository, IBasketRepository basketRepository)
		{
			_orderRepository = orderRepository;
			_basketRepository = basketRepository;
		}

		[HttpPost("submit")]
		public async Task<IActionResult> SubmitOrder(Guid customerId)
		{
			var basket = await _basketRepository.GetBasketByCustomerId(customerId);

			if (basket == null)
			{
				return NotFound("Basket not found");
			}

			var orderRequest = new OrderCreateRequestModel
			{
				CustomerId = customerId,
				OrderItems = basket.BasketItems.Select(item => new OrderItemRequestModel
				{
					ProductId = item.ProductId,
					ProductName = item.ProductName,
					Quantity = item.Quantity,
					Price = item.Price
				}).ToList(),
				TotalPrice = basket.TotalPrice
			};

			var orderResponse = await _orderRepository.SubmitOrder(orderRequest);

			// Optionally clear the basket after order submission
			await _basketRepository.ClearBasketByCustomerId(customerId);

			return Ok(orderResponse);
		}

		[HttpGet("{orderId}")]
		public async Task<IActionResult> GetOrderById(Guid orderId)
		{
			var order = await _orderRepository.GetOrderById(orderId);
			if (order == null)
			{
				return NotFound("Order not found");
			}
			return Ok(order);
		}

		[HttpDelete("delete/{orderId}")]
		public async Task<IActionResult> DeleteOrder(Guid orderId)
		{
			var result = await _orderRepository.DeleteOrder(orderId);
			if (!result)
			{
				return NotFound("Order not found");
			}

			return Ok("Order deleted successfully");
		}
	}
}