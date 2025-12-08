using FeatureEnvy.Model;

namespace FeatureEnvy.Services;

public class OrderService
{
    public Order CreateOrder(Basket basket, Address shippingAddress)
    {
        var order = new Order();
        order.ShippingAddress = shippingAddress;

        foreach (var item in basket.Items)
        {
            order.Items.Add(item);
            order.Total += item.Product.UnitPrice * item.Quantity;
        }

        return order;
    }
}
