using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactEShop.Models.Dto;

public sealed record CustomerDetailsResponseModel
{
	public required Guid Id { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
	public required string Email { get; set; }
	public required string PhoneNumber { get; set; }
	public required string Address { get; set; }
	public required string PostalCode { get; set; }
}
