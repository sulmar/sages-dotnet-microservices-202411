using FluentValidation;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Validators;

public class CartItemValidator : AbstractValidator<CartItem>
{
    private readonly IShoppingCartRepository repository;

    public CartItemValidator(IShoppingCartRepository repository)
    {
        RuleFor(p => p.Price).InclusiveBetween(1, 1000);
        this.repository = repository;
    }
}
