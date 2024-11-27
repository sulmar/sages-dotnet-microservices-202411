using MediatR;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Events;

namespace ShoppingCart.Api.Handlers;

public class GenerateDocumentHandler(IDocumentService documentService) : INotificationHandler<CheckoutEvent>
{
    public Task Handle(CheckoutEvent notification, CancellationToken cancellationToken)
    {
        documentService.Generate();

        return Task.CompletedTask;
    }
}