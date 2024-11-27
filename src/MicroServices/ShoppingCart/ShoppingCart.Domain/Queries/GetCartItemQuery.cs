using MediatR;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Queries;

public record GetCartItemQuery(int id) : IRequest<CartItem>;
