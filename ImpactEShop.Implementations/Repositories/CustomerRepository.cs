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
	public class CustomerRepository : ICustomerRepository
	{
		private readonly AppDbContext _dbContext;

		public CustomerRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
		{
			return await _dbContext.Customers.FindAsync(customerId);
		}

		public async Task<List<Customer>> GetCustomersAsync()
		{
			return await _dbContext.Customers.ToListAsync();
		}

		public async Task CreateCustomerAsync(Customer customer)
		{
			await _dbContext.Customers.AddAsync(customer);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateCustomerAsync(Customer customer)
		{
			_dbContext.Customers.Update(customer);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteCustomerAsync(Guid customerId)
		{
			var customer = await GetCustomerByIdAsync(customerId);
			if (customer != null)
			{
				_dbContext.Customers.Remove(customer);
				await _dbContext.SaveChangesAsync();
			}
		}

		/*public async Task<Customer> GetCustomerByEmailAsync(string email)
		{
			return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
		}*/

		/*public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			_dbContext.Customers.Add(customer);
			await _dbContext.SaveChangesAsync();
			return customer;
		}*/

		
	}
}