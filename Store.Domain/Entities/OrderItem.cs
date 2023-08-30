using Store.Domain.Entities.Contracts;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem(Product product, int quantity)
    {
        AddNotifications(new CreateProductContract(product, quantity));

        Product = product;
        Quantity = quantity;
        Price = Product != null ? Product.Price : 0;
    }

    public Product? Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public decimal SubTotal()
    {
        return Price * Quantity;
    }
}