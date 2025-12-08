namespace LongParameterList;

public class Order
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
    public DateTime OrderDate { get; set; }

    public decimal TotalAmount => Quantity * Price;
}

public class OrderService
{
    public Order CreateOrder(
        string customerName,
        string customerEmail,
        string productName,
        int quantity,
        decimal price,
        string shippingAddress,
        string billingAddress,
        DateTime orderDate)
    {
        var order = new Order
        {
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            ProductName = productName,
            Quantity = quantity,
            Price = price,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            OrderDate = orderDate
        };

        return order;
    }
}
