using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("John Doe", "johndoe@example.com");
        private readonly Product _product = new Product("Product 01", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenValidNewOrder_ItShouldGenerateAn8CharacterNumber()
        {
            // Arrange
            var order = new Order(_customer, 0, null);

            // Assert
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenNewOrder_StatusShouldBeWaitingForPayment()
        {
            // Arrange
            var order = new Order(_customer, 0, null);

            // Assert
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenPaymentForOrder_StatusShouldBeWaitingForDelivery()
        {
            // Arrange
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);

            // Act
            order.Pay(10);

            // Assert
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenCancelledOrder_StatusShouldBeCancelled()
        {
            // Arrange
            var order = new Order(_customer, 0, null);

            // Act
            order.Cancel();

            // Assert
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenNewItemWithoutProduct_ItShouldNotBeAdded()
        {
            // Arrange
            var order = new Order(_customer, 0, null);

            // Act
            order.AddItem(null, 10);

            // Assert
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenNewItemWithZeroOrNegativeQuantity_ItShouldNotBeAdded()
        {
            // Arrange
            var order = new Order(_customer, 0, null);

            // Act
            order.AddItem(_product, 0);

            // Assert
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenValidNewOrder_TotalShouldBe50()
        {
            // Arrange
            var order = new Order(_customer, 10, _discount);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenExpiredDiscount_OrderValueShouldBe60()
        {
            // Arrange
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, expiredDiscount);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenInvalidDiscount_OrderValueShouldBe60()
        {
            // Arrange
            var order = new Order(_customer, 10, null);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Given10Discount_OrderValueShouldBe50()
        {
            // Arrange
            var order = new Order(_customer, 10, _discount);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenDeliveryFeeOf10_OrderValueShouldBe50()
        {
            // Arrange
            var order = new Order(_customer, 10, _discount);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenOrderWithoutCustomer_ItShouldBeInvalid()
        {
            // Arrange
            var order = new Order(null, 10, _discount);

            // Assert
            Assert.AreEqual(order.IsValid, false);
        }
    }
}
