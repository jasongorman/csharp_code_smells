using FeatureEnvy.Model;
using FeatureEnvy.Services;

namespace FeatureEnvy.Test;

using NUnit.Framework;
using System.Collections.Generic;

public class OrderConfirmationServiceTests
{
    private OrderConfirmationService _service;
    private StockService _stockService;
    private List<WarehouseStock> _stocks;
    private Order _order;
    private Product _product;

    [SetUp]
    public void Setup()
    {
        _product = new Product { Name = "Camera", UnitPrice = 800m };

        _stocks = new List<WarehouseStock>
        {
            new() { Product = _product, Quantity = 5 }
        };

        _stockService = new StockService(_stocks);
        _service = new OrderConfirmationService(_stockService);

        _order = new Order();
    }

    [Test]
    public void ConfirmOrder_AllItemsInStock_ReturnsTrue()
    {
        _order.Items.Add(new Item { Product = _product, Quantity = 2 });

        var result = _service.ConfirmOrder(_order);

        Assert.IsTrue(result);
        Assert.IsTrue(_order.Confirmed);
    }

    [Test]
    public void ConfirmOrder_ItemOutOfStock_ReturnsFalse()
    {
        _order.Items.Add(new Item { Product = _product, Quantity = 10 });

        var result = _service.ConfirmOrder(_order);

        Assert.IsFalse(result);
        Assert.IsFalse(_order.Confirmed);
    }
}