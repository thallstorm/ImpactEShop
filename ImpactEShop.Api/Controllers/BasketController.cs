using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ImpactEShop.Api.Controllers
{
	public class BasketController : ControllerBase //inherits from ControllerBase (ASP.NET Core MVC framework for handling web API requests)
	{
		private readonly IBasketRepository _basketRepository;		//Field declared with type IBasketRepository, this means it can hold any object that implements the IBasketRepository Interface.
																	//The underscore indicates a private member variable.

		public BasketController(IBasketRepository basketRepository) //Dependency Injection. The constructor takes the parameter basketRepository of type IBasketRepository
																	//This Parameter is used to inject the actual repository implementation that will be used by the Controller															
		{                                                           //The Controller doesn't create the repository itself, it relies on an external system to provide it
																	//This promotes loose coupling and easier testing.
			_basketRepository = basketRepository;                   //The _basketRepository field is assigned the value of the basketRepository parameter
		}															//This injects the provided repository dependency

		[HttpGet]
		[Route("api/basket/{customerId}")]
		public async Task<IActionResult> GetBasketByCustomerId(Guid customerId)			   //returns an object of type IActionResult, which is an interface that allows returning different HTTP responses.
		{																				   //IActionResult is more generic than ActionResult<T>, allowing various response types
																						   //It is also more flexible, can return different data types
			var basketDetails = await _basketRepository.GetBasketByCustomerId(customerId); //uses injected _basketRepository obj to call async method
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
