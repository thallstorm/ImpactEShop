using ImpactEShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Abstractions.Repositories
{
	public interface ICustomerRepository
	{
		Task<Customer> GetCustomerByIdAsync(Guid customerId);
		Task<List<Customer>> GetCustomersAsync();
		Task CreateCustomerAsync(Customer customer);
		Task UpdateCustomerAsync(Customer customer);
		Task DeleteCustomerAsync(Guid customerId);
	}
}