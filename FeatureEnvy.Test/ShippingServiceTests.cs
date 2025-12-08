using FeatureEnvy.Model;
using FeatureEnvy.Services;

namespace FeatureEnvy.Test;

using NUnit.Framework;

public class ShippingServiceTests
{
    private ShippingService _service;
    private Order _order;

    [SetUp]
    public void Setup()
    {
        _service = new ShippingService();
        _order = new Order();
    }

    [TestCase("UK", "UK")]
    [TestCase("DE", "EU")]
    [TestCase("FR", "EU")]
    [TestCase("US", "Other")]
    [TestCase("CN", "Other")]
    public void CreateShippingNote_DeterminesZone(string country, string expected)
    {
        _order.ShippingAddress = new Address { Country = country };

        var note = _service.CreateShippingNote(_order);

        Assert.That(note.ShippingZone, Is.EqualTo(expected));
    }
}