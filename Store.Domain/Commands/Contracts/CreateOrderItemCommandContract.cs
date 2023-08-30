using Flunt.Validations;
using Store.Domain.Commands;

namespace Store.Domain.Entities.Contracts;
public class CreateOrderItemCommandContract : Contract<CreateOrderItemCommand>
{
    public CreateOrderItemCommandContract(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;

        Requires()
        .AreEquals(Product.ToString(), 32, "Product", "Produto inválido")
        .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida");
    }

    public Guid Product { get; set; }
    public int Quantity { get; set; }
}