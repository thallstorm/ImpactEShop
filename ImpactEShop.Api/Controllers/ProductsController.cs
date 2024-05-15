﻿using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Implementations.Repositories;
using ImpactEShop.Models.Domain;
using ImpactEShop.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImpactShopExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsRepository _productsRepository;

		public ProductsController(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		[HttpGet("{productId}")]
		public async Task<ActionResult<ProductDetailsResponseModel>> GetProductByIdAsync(Guid productId)
		{
			var product = await _productsRepository.GetProductByIdAsync(productId);
			if (product == null)
			{
				return NotFound();
			}

			// Map product data to response model
			var responseModel = new ProductDetailsResponseModel
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Brand = product.Brand,
				DiscountedPrice = product.DiscountedPrice,
				Stock = product.Stock,
				Status = product.Status
			};

			return Ok(responseModel);
		}

		[HttpGet]
		public async Task<ActionResult<ProductListResponseModel>> GetProductsAsync(int page = 1, int pageSize = 10,
			string brandFilter = null, decimal? minPrice = null, decimal? maxPrice = null)
		{
			// Ensure valid page and page size (allow 6, 12, or all)
			page = Math.Max(page, 1);
			pageSize = pageSize == int.MaxValue ? int.MaxValue : Math.Clamp(pageSize, 6, 12); 

			// Apply filtering logic
			var filter = new ProductFilter
			{
				Brand = brandFilter,
				MinPrice = minPrice,
				MaxPrice = maxPrice
			};

			// Call repository to get total product count based on filters
			var totalProducts = await _productsRepository.GetTotalProductCountAsync(filter);

			// Create and return the response model with pagination information
			var responseModel = new ProductListResponseModel
			{
				CurrentPage = page,
				PageSize = pageSize,
				TotalProducts = totalProducts,
				Products = new List<ProductSummaryResponseModel>() // Empty list for products
			};

			// Retrieve product details for the current page using a separate repository call (optional)
			/*var productDetails = await _productsRepository.GetProductsAsync(page, pageSize); // Optional

			// Map product details to ProductSummaryResponseModel objects and add them to the response
			foreach (var product in productDetails)
			{
				responseModel.Products.Add(new ProductSummaryResponseModel
				{
					Id = product.Id,
					Name = product.Name,
					Price = product.Price,
					Brand = product.Brand
					//ImageUrl = Your logic to get the image URL for the product (optional)

				});
			}*/

			return Ok(responseModel);
		}

		[HttpPost]
		public async Task<ActionResult<ProductDetailsResponseModel>> CreateProductAsync([FromBody] ProductCreateRequestModel requestModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Map request model data to product object
			var product = new Product
			{
				Id = requestModel.Id,
				Name = requestModel.Name,
				Description = requestModel.Description,
				Price = requestModel.Price,
				Brand = requestModel.Brand,
				DiscountedPrice = requestModel.DiscountedPrice,
				Stock = requestModel.Stock,
				Status = requestModel.Status
			};

			var createdProduct = await _productsRepository.CreateProductAsync(product);

			// Map created product data to response model
			var responseModel = new ProductDetailsResponseModel
			{
				Id = createdProduct.Id,
				Name = createdProduct.Name,
				Description = createdProduct.Description,
				Price = createdProduct.Price,
				Brand = createdProduct.Brand,
				DiscountedPrice = createdProduct.DiscountedPrice,
				Stock = createdProduct.Stock,
				Status = createdProduct.Status
			};

			return CreatedAtRoute("GetProduct", new { productId = responseModel.Id }, responseModel);
		}

		// Implement UpdateProductAsync and DeleteProductAsync methods
		// using ProductUpdateRequestModel (optional) and appropriate response models
	}
}