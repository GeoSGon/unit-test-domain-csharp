using Flunt.Notifications;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities.Contracts;

namespace Store.Domain.Commands;

public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderItemCommand() 
    {}

    public CreateOrderItemCommand(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Guid Product { get; set; } 
    public int Quantity { get; set; }

    public void Validate()
    {
        AddNotifications(new CreateOrderItemCommandContract(Product, Quantity));
    }
}
