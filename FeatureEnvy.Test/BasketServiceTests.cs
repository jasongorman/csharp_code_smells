using FeatureEnvy.Model;
using FeatureEnvy.Services;

namespace FeatureEnvy.Test;

using NUnit.Framework;
using System.Linq;

public class BasketServiceTests
{
    private BasketService _service;
    private Basket _basket;
    private Product _product;

    [SetUp]
    public void Setup()
    {
        _service = new BasketService();
        _basket = new Basket();
        _product = new Product { Name = "Laptop", UnitPrice = 1000m };
    }

    [Test]
    public void AddToBasket_NewProduct_AddsItem()
    {
        _service.AddToBasket(_basket, _product, 2);

        Assert.That(_basket.Items.Count, Is.EqualTo(1));
        Assert.That(_basket.Items.First().Quantity, Is.EqualTo(2));
    }

    [Test]
    public void AddToBasket_ExistingProduct_IncrementsQuantity()
    {
        _service.AddToBasket(_basket, _product, 2);
        _service.AddToBasket(_basket, _product, 3);

        Assert.That(_basket.Items.Count, Is.EqualTo(1));
        Assert.That(_basket.Items.First().Quantity, Is.EqualTo(5));
    }
}