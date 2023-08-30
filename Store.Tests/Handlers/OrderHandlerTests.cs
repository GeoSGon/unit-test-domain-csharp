using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;
[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderHandlerTests()
    {
        // Arrange
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _orderRepository = new FakeOrderRepository();
        _productRepository = new FakeProductRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void GivenNonexistentCustomer_OrderShouldNotBeGenerated()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            Customer = null,
            ZipCode = "12345678",
            PromoCode = "12345678",
            Items = { new CreateOrderItemCommand(Guid.NewGuid(), 1) }
        };

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _orderRepository,
            _productRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void GivenNonexistentZipCode_OrderShouldBeGenerated()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            Customer = "John Doe",
            ZipCode = null,
            PromoCode = "12345678",
            Items = { new CreateOrderItemCommand(Guid.NewGuid(), 1) }
        };

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _orderRepository,
            _productRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void GivenNonexistentPromoCode_OrderShouldBeGenerated()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            Customer = "John Doe",
            ZipCode = "12345678",
            PromoCode = null,
            Items = { new CreateOrderItemCommand(Guid.NewGuid(), 1) }
        };

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _orderRepository,
            _productRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void GivenOrderWithoutItems_OrderShouldNotBeGenerated()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            Customer = "John Doe",
            ZipCode = "12345678",
            PromoCode = "12345678",
            Items = null
        };

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _orderRepository,
            _productRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void GivenInvalidCommand_OrderShouldNotBeGenerated()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            Customer = null,
            ZipCode = "12345678",
            PromoCode = "12345678",
            Items = { new CreateOrderItemCommand(Guid.NewGuid(), 1) }
        };

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _orderRepository,
            _productRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.AreEqual(handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void GivenValidCommand_OrderShouldBeGenerated()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            Customer = "John Doe",
            ZipCode = "12345678",
            PromoCode = "12345678",
            Items = { new CreateOrderItemCommand(Guid.NewGuid(), 1) }
        };

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _orderRepository,
            _productRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.AreEqual(handler.IsValid, true);
    }
}
