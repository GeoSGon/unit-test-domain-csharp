using Flunt.Validations;
using Store.Domain.Commands;
namespace Store.Domain.Entities.Contracts;
public class CreateOrderCommandContract : Contract<CreateOrderCommand>
{
    public CreateOrderCommandContract(string customer, string zipCode)
    {
        Customer = customer;
        ZipCode = zipCode;

        Requires()
        .AreEquals(customer, 11, "Customer", "Cliente inválido")
        .AreEquals(zipCode, 8, "ZipCode", "CEP inválido");
    }

    public string? Customer { get; set; }
    public string? ZipCode { get; set; }
}