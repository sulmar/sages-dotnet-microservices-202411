using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Abstractions;

public interface IMessageService
{
    void Send(Message message);
}


    