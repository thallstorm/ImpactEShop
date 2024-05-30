using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using Mapster;

namespace ImpactEShop.Api.Configuration
{
	public static class MapsterConfiguration
	{
		public static void Configure()
		{
			TypeAdapterConfig<Basket, BasketDetailsResponseModel>
				.NewConfig()
				.Map(dest => dest.TotalPrice, src => src.Price)
				.Map(dest => dest.BasketItems, src => src.BasketItems.Adapt<List<BasketDetailsResponseModel.BasketItemDetails>>());

			TypeAdapterConfig<BasketItem, BasketDetailsResponseModel.BasketItemDetails>
				.NewConfig()
				.Map(dest => dest.ProductId, src => src.ProductId)
				.Map(dest => dest.ProductName, src => src.Product.Name)
				.Map(dest => dest.Quantity, src => src.Quantity)
				.Map(dest => dest.Price, src => src.Price);
		}
	}
}
