using MediatR;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Events;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Api.Handlers;

public class SendMessageHandler(IMessageService messageService) : INotificationHandler<CheckoutEvent>
{
    public Task Handle(CheckoutEvent notification, CancellationToken cancellationToken)
    {
        var message = new Message { Title = "Hello World!" };
        messageService.Send(message);

        return Task.CompletedTask;
    }
}
