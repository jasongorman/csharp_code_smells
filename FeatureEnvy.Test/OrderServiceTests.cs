using FeatureEnvy.Model;
using FeatureEnvy.Services;

namespace FeatureEnvy.Test;

public class OrderServiceTests
{
    private OrderService _service;
    private Basket _basket;
    private Address _address;

    [SetUp]
    public void Setup()
    {
        _service = new OrderService();
        _basket = new Basket();
        _address = new Address { Country = "UK" };

        _basket.Items.Add(new Item
        {
            Product = new Product { Name = "Phone", UnitPrice = 500m },
            Quantity = 2
        });

        _basket.Items.Add(new Item
        {
            Product = new Product { Name = "Mouse", UnitPrice = 50m },
            Quantity = 1
        });
    }

    [Test]
    public void CreateOrder_CopiesItemsAndCalculatesTotal()
    {
        var order = _service.CreateOrder(_basket, _address);

        Assert.That(order.Items.Count, Is.EqualTo(2));
        Assert.That(order.Total, Is.EqualTo(1050m));
        Assert.That(order.ShippingAddress.Country, Is.EqualTo("UK"));
    }
}