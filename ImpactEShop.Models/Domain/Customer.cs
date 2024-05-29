using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Domain
{
	public sealed class Customer
	{
		public required Guid Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string PhoneNumber { get; set; }
		public required string Address { get; set; }
		public required string PostalCode { get; set; }
	}
}