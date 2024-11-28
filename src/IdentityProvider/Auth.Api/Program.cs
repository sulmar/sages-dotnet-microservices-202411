using Auth.Api.Abstractions;
using Auth.Api.Infrastructures;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// polecam: BCrypt
builder.Services.AddTransient<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITokenService, JwtTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/", () => "Hello Auth.Api!");

app.MapPost("api/login", async (LoginModel model, IAuthService authService, ITokenService tokenService, HttpContext context) =>
{
    var result = await authService.AuthorizeAsync(model.Username, model.Password);

    if (result.isAuthorized)
    {
        var accessToken = tokenService.CreateAccessToken(result.identity);

        context.Response.Cookies.Append("access-token", accessToken, new CookieOptions
        {

        });

        return Results.Ok(accessToken);
    }

    return Results.Unauthorized();
});

app.Run();

