using ImpactEShop.Models.Dto;
using Microsoft.AspNetCore.Http;
using ImpactEShop.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Domain;
using Mapster;

namespace ImpactEShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerController(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		[HttpGet("{customerId}")]
		public async Task<ActionResult<CustomerDetailsResponseModel>> GetCustomerByIdAsync(Guid customerId) //Async method that returns an ActionResult<T> type Obj
		{																				//ActionResult<T> class (inherits from IActionResult) is more specific, enforcing consistent data model
			var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
			if (customer == null)
			{
				return NotFound();
			}

			// Map customer data to response model using Mapster
			var responseModel = customer.Adapt<CustomerDetailsResponseModel>();

			return Ok(responseModel);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerDetailsResponseModel>> CreateCustomerAsync([FromBody] CustomerCreateRequestModel requestModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Map request model data to customer object using Mapster
			var customer = requestModel.Adapt<Customer>();

			await _customerRepository.CreateCustomerAsync(customer);

			// Map created customer data to response model using Mapster
			var responseModel = customer.Adapt<CustomerDetailsResponseModel>();

			return Ok(responseModel);
		}

		[HttpGet]
		public async Task<ActionResult<List<CustomerDetailsResponseModel>>> GetCustomersAsync()
		{
			var customers = await _customerRepository.GetCustomersAsync();

			var responseModels = customers.Adapt<List<CustomerDetailsResponseModel>>();


			return Ok(responseModels);
		}

		[HttpPut("{customerId}")]
		public async Task<ActionResult<CustomerDetailsResponseModel>> UpdateCustomerAsync(Guid customerId, [FromBody] CustomerCreateRequestModel requestModel)
		{
			if (requestModel == null)
			{
				return BadRequest("Request model cannot be null.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
			if (customer == null)
			{
				return NotFound();
			}

			// Map request model data to existing customer object using Mapster
			requestModel.Adapt(customer);

			await _customerRepository.UpdateCustomerAsync(customer);

			// Map updated customer data to response model using Mapster
			var responseModel = customer.Adapt<CustomerDetailsResponseModel>();

			return Ok(responseModel);
		}

		[HttpDelete("{customerId}")]
		public async Task<IActionResult> DeleteCustomerAsync(Guid customerId)
		{
			var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
			if (customer == null)
			{
				return NotFound();
			}

			await _customerRepository.DeleteCustomerAsync(customerId);

			return NoContent(); // Customer deleted successfully (no content to return)
		}

	}
}

