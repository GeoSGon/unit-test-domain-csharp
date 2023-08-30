using Flunt.Validations;

namespace Store.Domain.Entities.Contracts;

public class CreateCustomerContract : Contract<Customer>
{
    public CreateCustomerContract(Customer customer)
    {
        Requires()
        .IsNotNull(customer, "Customer", "Cliente inválido");
    }
}