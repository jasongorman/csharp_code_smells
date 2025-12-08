using FeatureEnvy.Model;
using FeatureEnvy.Services;

namespace FeatureEnvy.Test;

using NUnit.Framework;
using System.Collections.Generic;

public class StockServiceTests
{
    private StockService _service;
    private List<WarehouseStock> _stocks;
    private Product _product;

    [SetUp]
    public void Setup()
    {
        _product = new Product { Name = "Tablet", UnitPrice = 300m };

        _stocks = new List<WarehouseStock>
        {
            new WarehouseStock { Product = _product, Quantity = 10 }
        };

        _service = new StockService(_stocks);
    }

    [Test]
    public void CheckStock_EnoughStock_ReturnsTrue()
    {
        Assert.IsTrue(_service.CheckStock(_product, 5));
    }

    [Test]
    public void CheckStock_NotEnoughStock_ReturnsFalse()
    {
        Assert.IsFalse(_service.CheckStock(_product, 20));
    }

    [Test]
    public void CheckStock_NoStockRecord_ReturnsFalse()
    {
        var otherProduct = new Product { Name = "Headphones", UnitPrice = 100m };
        Assert.IsFalse(_service.CheckStock(otherProduct, 1));
    }
}