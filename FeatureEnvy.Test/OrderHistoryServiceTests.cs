using FeatureEnvy.Model;
using FeatureEnvy.Services;

namespace FeatureEnvy.Test;

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class OrderHistoryServiceTests
{
    private OrderHistoryService _service;
    private List<Order> _orders;
    private Product _product;
    private Address _address;

    [SetUp]
    public void Setup()
    {
        _product = new Product { Name = "TV", UnitPrice = 1500m };
        _address = new Address { Country = "UK" };

        _orders = new List<Order>
        {
            new Order
            {
                Confirmed = true,
                ShippingAddress = _address,
                Items = new List<Item>
                {
                    new Item { Product = _product, Quantity = 1 }
                }
            },
            new Order
            {
                Confirmed = false,
                ShippingAddress = _address,
                Items = new List<Item>
                {
                    new() { Product = _product, Quantity = 1 }
                }
            },
            new()
            {
                Confirmed = true,
                ShippingAddress = new Address { Country = "US" },
                Items = new List<Item>
                {
                    new Item { Product = new Product { Name = "Phone", UnitPrice = 800m }, Quantity = 1 }
                }
            }
        };

        _service = new OrderHistoryService(_orders);
    }

    [Test]
    public void FindOrdersByProduct_ReturnsOnlyConfirmedOrders()
    {
        var result = _service.FindOrdersByProduct(_product).ToList();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.IsTrue(result.First().Confirmed);
    }

    [Test]
    public void FindOrdersByAddress_ReturnsOnlyConfirmedOrders()
    {
        var result = _service.FindOrdersByAddress(_address).ToList();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.IsTrue(result.First().Confirmed);
        Assert.That(result.First().ShippingAddress.Country, Is.EqualTo("UK"));
    }
}