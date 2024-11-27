using MediatR;
using ShoppingCart.Domain.Events;
using ShoppingCart.Domain.Abstractions;

namespace ShoppingCart.Api.Handlers;

public class AddCartItemToRepositoryHandler(IShoppingCartRepository repository) : INotificationHandler<CheckoutEvent>
{
    public async Task Handle(CheckoutEvent notification, CancellationToken cancellationToken)
    {
        await repository.Add(notification.Item);
    }
}
