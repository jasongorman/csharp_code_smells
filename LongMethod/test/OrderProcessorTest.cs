namespace LongMethod.test
{
    namespace OrderProcessorTests
{
    [TestFixture]
    public class OrderProcessorTests
    {
        private OrderProcessor _processor;

        [SetUp]
        public void Setup()
        {
            _processor = new OrderProcessor();
        }

        [Test]
        public void NullOrder_ShouldThrowException()
        {
            Assert.Throws<InvalidOrderException>(() => _processor.ProcessOrder(null));
        }

        [Test]
        public void MissingCustomerName_ShouldReturnWarning()
        {
            var order = new Order { CustomerName = null, Items = new List<OrderItem>() };

            Assert.Throws<InvalidOrderException>(() => _processor.ProcessOrder(order));
        }

        [Test]
        public void EmptyItems_ShouldReturnWarning()
        {
            var order = new Order { CustomerName = "Alice", Items = new List<OrderItem>() };

            Assert.Throws<InvalidOrderException>(() => _processor.ProcessOrder(order));
        }

        [Test]
        public void Item_WithZeroOrNegativeQuantity_ShouldBeSkipped()
        {
            var order = new Order
            {
                CustomerName = "TestUser",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Widget", Quantity = 0, Price = 100m },   // invalid
                    new OrderItem { Name = "Gadget", Quantity = 2, Price = 50m }     // valid
                }
            };

            var invoice = _processor.ProcessOrder(order);

            Assert.Contains("Invalid quantity for item: Widget", invoice.Warnings);
            Assert.That(invoice.Items.Count, Is.EqualTo(1));
            Assert.That(invoice.Items[0].Name, Is.EqualTo("Gadget"));
            Assert.That(invoice.Items[0].Quantity, Is.EqualTo(2));
            Assert.That(invoice.Items[0].Price, Is.EqualTo(50m));
            Assert.That(invoice.Subtotal, Is.EqualTo(100m));
            Assert.That(invoice.Shipping, Is.EqualTo(5m)); // shipping tier
            Assert.That(invoice.Total, Is.EqualTo(105m));
        }

        [Test]
        public void Item_WithNegativePrice_ShouldBeSkipped()
        {
            var order = new Order
            {
                CustomerName = "TestUser",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Widget", Quantity = 1, Price = -50m },  // invalid
                    new OrderItem { Name = "Gadget", Quantity = 2, Price = 50m }    // valid
                }
            };

            var invoice = _processor.ProcessOrder(order);

            Assert.Contains("Invalid price for item: Widget", invoice.Warnings);
            Assert.That(invoice.Items.Count, Is.EqualTo(1));
            Assert.That(invoice.Items[0].Name, Is.EqualTo("Gadget"));
            Assert.That(invoice.Subtotal, Is.EqualTo(100m));
            Assert.That(invoice.Shipping, Is.EqualTo(5m));
            Assert.That(invoice.Total, Is.EqualTo(105m));
        }

        [Test]
        public void AppliesDiscount_WhenTotalOver500()
        {
            var order = new Order
            {
                CustomerName = "RichCustomer",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Gold Bar", Quantity = 1, Price = 600m }
                }
            };

            var invoice = _processor.ProcessOrder(order);

            // Discount = 10% of 600 = 60
            Assert.That(invoice.Subtotal, Is.EqualTo(600m));
            Assert.That(invoice.Discount, Is.EqualTo(60m));
            Assert.That(invoice.Shipping, Is.EqualTo(0m));  // total after discount = 540 > 200
            Assert.That(invoice.Total, Is.EqualTo(540m));
        }

        [Test]
        public void AppliesDiscount_WhenTotalOver200()
        {
            var order = new Order
            {
                CustomerName = "Customer",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Item1", Quantity = 1, Price = 300m } // subtotal > 200
                }
            };

            var invoice = _processor.ProcessOrder(order);

            // Discount = 5% of 300 = 15
            Assert.That(invoice.Subtotal, Is.EqualTo(300m));
            Assert.That(invoice.Discount, Is.EqualTo(15m));
            Assert.That(invoice.Shipping, Is.EqualTo(0m));  // total after discount = 285 > 200
            Assert.That(invoice.Total, Is.EqualTo(285m));
        }

        [Test]
        public void Shipping_Is10_WhenTotalUnder50()
        {
            var order = new Order
            {
                CustomerName = "SmallOrder",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Cheap", Quantity = 1, Price = 20m }
                }
            };

            var invoice = _processor.ProcessOrder(order);

            Assert.That(invoice.Subtotal, Is.EqualTo(20m));
            Assert.That(invoice.Discount, Is.EqualTo(0m));
            Assert.That(invoice.Shipping, Is.EqualTo(10m));
            Assert.That(invoice.Total, Is.EqualTo(30m));
        }

        [Test]
        public void Shipping_Is5_WhenTotalBetween50And200()
        {
            var order = new Order
            {
                CustomerName = "MediumOrder",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Item", Quantity = 1, Price = 150m }
                }
            };

            var invoice = _processor.ProcessOrder(order);

            Assert.That(invoice.Subtotal, Is.EqualTo(150m));
            Assert.That(invoice.Discount, Is.EqualTo(0m));
            Assert.That(invoice.Shipping, Is.EqualTo(5m));
            Assert.That(invoice.Total, Is.EqualTo(155m));
        }

        [Test]
        public void Shipping_Is0_WhenTotalOver200()
        {
            var order = new Order
            {
                CustomerName = "BigOrder",
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Expensive", Quantity = 1, Price = 300m }
                }
            };

            var invoice = _processor.ProcessOrder(order);

            Assert.That(invoice.Subtotal, Is.EqualTo(300m));
            Assert.That(invoice.Discount, Is.EqualTo(15m)); // 5% discount
            Assert.That(invoice.Shipping, Is.EqualTo(0m));
            Assert.That(invoice.Total, Is.EqualTo(285m));
        }
    }
}

}