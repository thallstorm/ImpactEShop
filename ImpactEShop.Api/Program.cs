using ImpactEShop.Abstractions.Repositories;
using ImpactEShop.Implementations.Repositories;
using ImpactEShop.Models.Data;
using ImpactEShop.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register services with dependency injection
builder.Services.AddTransient<IProductsRepository, ProductsRepository>(); // Replace with your actual product service implementation
builder.Services.AddSingleton<IBasketRepository>(provider => new BasketRepository(
	new ConcurrentDictionary<Guid, List<BasketItem>>(), // Replace with your basket storage implementation
	provider.GetRequiredService<IProductsRepository>()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
