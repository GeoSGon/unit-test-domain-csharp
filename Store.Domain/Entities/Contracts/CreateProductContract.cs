using Flunt.Validations;

namespace Store.Domain.Entities.Contracts;

public class CreateProductContract : Contract<OrderItem>
{
    public CreateProductContract(Product product, int quantity)
    {
        Requires()
        .IsNotNull(product, "Product", "Produto inválido")
        .IsGreaterThan(quantity, 0, "Quantity", "A quantidade deve ser maior que zero");
    }
}