using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Models;
using ShoppingCart.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IShoppingCartRepository, FakeShoppingCartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello ShoppingCart.Api");

app.MapPost("api/cart", async (CartItem item, IShoppingCartRepository repository) =>
{
    await repository.Add(item);

    return Results.Created();
});


app.Run();

