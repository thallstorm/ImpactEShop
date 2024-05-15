using ImpactEShop.Models.Dto;
using Microsoft.AspNetCore.Http;
using ImpactEShop.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Models.Domain;

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
		public async Task<ActionResult<CustomerDetailsResponseModel>> GetCustomerByIdAsync(Guid customerId)
		{
			var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
			if (customer == null)
			{
				return NotFound();
			}

			// Map customer data to response model
			var responseModel = new CustomerDetailsResponseModel
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				PhoneNumber = customer.PhoneNumber,
				Address = customer.Address,
				PostalCode = customer.PostalCode,
				Email = customer.Email
			};

			return Ok(responseModel);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerDetailsResponseModel>> CreateCustomerAsync([FromBody] CustomerCreateRequestModel newCustomerRequestModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Map request model data to customer object
			var customer = new Customer
			{
				Id = newCustomerRequestModel.Id,
				Email = newCustomerRequestModel.Email,
				FirstName = newCustomerRequestModel.FirstName,
				LastName = newCustomerRequestModel.LastName,
				PhoneNumber = newCustomerRequestModel.PhoneNumber,
				Address = newCustomerRequestModel.Address,
				PostalCode = newCustomerRequestModel.PostalCode
			};

			await _customerRepository.CreateCustomerAsync(customer);

			// Map created customer data to response model
			var responseModel = new CustomerDetailsResponseModel
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PhoneNumber = customer.PhoneNumber,
				Address = customer.Address,
				PostalCode = customer.PostalCode
			};

			return Ok(responseModel);
		}

		[HttpGet]
		public async Task<ActionResult<List<CustomerDetailsResponseModel>>> GetCustomersAsync()
		{
			var customers = await _customerRepository.GetCustomersAsync();
			var responseModels = customers.Select(c => new CustomerDetailsResponseModel
			{
				Id = c.Id,
				FirstName = c.FirstName,
				LastName = c.LastName,
				Email = c.Email,
				PhoneNumber = c.PhoneNumber,
				Address = c.Address,
				PostalCode = c.PostalCode
			}).ToList();

			return Ok(responseModels);
		}

		[HttpPut("{customerId}")]
		public async Task<ActionResult<CustomerDetailsResponseModel>> UpdateCustomerAsync(Guid customerId, [FromBody] CustomerCreateRequestModel requestModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
			if (customer == null)
			{
				return NotFound();
			}

			customer.FirstName = requestModel.FirstName;
			customer.LastName = requestModel.LastName;
			customer.Email = requestModel.Email;
			customer.PhoneNumber = requestModel.PhoneNumber;
			customer.Address = requestModel.Address;

			await _customerRepository.UpdateCustomerAsync(customer);

			var responseModel = new CustomerDetailsResponseModel
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PhoneNumber = customer.PhoneNumber,
				Address = customer.Address,
				PostalCode = customer.PostalCode
			};

			return Ok(responseModel);
		}

		[HttpDelete("{customerId}")]
		public async Task<IActionResult> DeleteCustomerAsync(Guid customerId)
		{
			await _customerRepository.DeleteCustomerAsync(customerId);

			return NoContent(); // Customer deleted successfully (no content to return)
		}

	}
}

