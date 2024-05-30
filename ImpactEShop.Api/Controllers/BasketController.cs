using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ImpactEShop.Api.Controllers
{
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;

		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet]
		[Route("api/basket/{customerId}")]
		public async Task<IActionResult> GetBasketByCustomerId(Guid customerId)
		{
			var basketDetails = await _basketRepository.GetBasketByCustomerId(customerId);
			if (basketDetails == null)
			{
				return NotFound();
			}
			return Ok(basketDetails);
		}

		[HttpPost]
		[Route("api/basket/{customerId}/addItem")]
		public async Task<IActionResult> AddItemToBasket(Guid customerId, Guid productId, int quantity)
		{
			if (quantity <= 0)
			{
				return BadRequest("Invalid quantity");
			}

			try
			{
				var basket = await _basketRepository.AddItemToBasket(customerId, productId, quantity);
				return Ok(basket);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("api/basket/{customerId}/updateItem")]
		public async Task<IActionResult> UpdateBasketItemQuantity(Guid customerId, Guid productId, int quantity)
		{
			if (quantity <= 0)
			{
				return BadRequest("Invalid quantity");
			}

			try
			{
				await _basketRepository.UpdateBasketItemQuantity(customerId, productId, quantity);
				return Ok();
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("api/basket/{customerId}/removeItem")]
		public async Task<IActionResult> RemoveItemFromBasket(Guid customerId, Guid productId)
		{
			try
			{
				await _basketRepository.UpdateBasketItemQuantity(customerId, productId, 0);
				return Ok();
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("api/basket/{customerId}")]
		public async Task<IActionResult> ClearBasketByCustomerId(Guid customerId)
		{
			var success = await _basketRepository.ClearBasketByCustomerId(customerId);
			if (success)
			{
				return Ok();
			}
			return NotFound();
		}
	}
}
