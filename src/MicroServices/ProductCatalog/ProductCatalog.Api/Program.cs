using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Api.Mappers;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProductRepository, FakeProductRepository>();
builder.Services.AddTransient<Context>(sp =>
{
    var products = new List<Product>
    {
        new Product(1, "Popular Product", 80.00m),
        new Product(2, "Special Item", 80.00m, 50m),
        new Product(3, "Extra Product", 80.00m),
        new Product(4, "Bonus Product", 80.00m, 70m),
        new Product(5, "Fancy Product", 80.00m, 70m),
        new Product(6, "Smart Product", 99.99m),
        new Product(7, "Old-school Product", 199.00m),
        new Product(8, "Future Product", 1.00m)
    };

    return new Context { Products = products.ToDictionary(p => p.Id) };
});

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.WithOrigins("https://localhost:7108");
    policy.WithMethods("GET");
    policy.AllowAnyHeader();

}));

string secretkey = "your-secret-key-your-secret-key-your-secret-key";

// dotnet add package FastEndpoints.Security
builder.Services.AddFastEndpoints();
    // .AddJWTBearerAuth(secretkey);

builder.Services.AddTransient<ProductMapper>();

// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access-token"];
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "your-issuer",

            ValidateAudience = true,
            ValidAudience = "your-audience",

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
        };

    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello ProductCatalog.Api");

app.MapGet("/api/products/map/{id:int}", async (IProductRepository repository, int id, ProductMapper mapper) =>
{
    var product = await repository.GetById(id);

    return Results.Ok(mapper.Map(product));
}).RequireAuthorization();

// PUT / PATCH

app.MapFastEndpoints();

app.Run();

