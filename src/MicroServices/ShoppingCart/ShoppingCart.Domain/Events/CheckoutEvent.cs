using MediatR;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Events;

public record CheckoutEvent(CartItem Item) : INotification;