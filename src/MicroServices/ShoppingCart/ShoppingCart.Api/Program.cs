using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Events;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Queries;
using ShoppingCart.Domain.Validators;
using ShoppingCart.Infrastructure;
using StackExchange.Redis;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddTransient<IShoppingCartRepository, FakeShoppingCartRepository>(); 
builder.Services.AddTransient<IShoppingCartRepository, RedisShoppingCartRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddTransient<IMessageService, EmailMessageService>();
builder.Services.AddTransient<IDocumentService, PdfDocumentService>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CartItem>, CartItemValidator>();


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


app.MapGet("api/cart/{id:int}", async (int id, IMediator mediator) => await mediator.Send(new GetCartItemQuery(id)));

app.MapPost("/api/cart/checkout", async (CartItem item, IMediator mediator, IValidator<CartItem> validator) =>
{
    var validationResult = await validator.ValidateAsync(item);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    await mediator.Publish(new CheckoutEvent(item));

    return Results.Ok();

});



app.Run();

