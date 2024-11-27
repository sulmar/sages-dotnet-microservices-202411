using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Infrastructure;

public class EmailMessageService : IMessageService
{
    public void Send(Message message)
    {
        Console.WriteLine(message);
    }
}
