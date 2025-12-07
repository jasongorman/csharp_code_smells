namespace LongMethod;

using System.Collections.Generic;

public class OrderProcessor
{
    public Invoice ProcessOrder(Order order)
    {
        // check if order is invalid
        
        Validate(order);

        var invoice = new Invoice
        {
            CustomerName = order?.CustomerName,
            Items = new List<InvoiceItem>(),
            Warnings = new List<string>()
        };

        // subtotal order items
        
        var subtotal = SubtotalOrder(order, invoice);

        // Apply discount
        
        decimal discount = 0;
        
        // 10% discount on orders over £500
        if (subtotal > 500) discount = subtotal * 0.10m;
        // 5% discount on orders over £200, up to £500
        else if (subtotal > 200) discount = subtotal * 0.05m;

        invoice.Discount = discount;

        decimal totalAfterDiscount = subtotal - discount;

        // Shipping
        decimal shipping = 0; // shipping is free on orders >= £200
        if (totalAfterDiscount < 50) shipping = 10;
        else if (totalAfterDiscount < 200) shipping = 5;

        decimal total = totalAfterDiscount + shipping;

        invoice.Subtotal = subtotal;
        invoice.Shipping = shipping;
        invoice.Total = total;

        return invoice;
    }

    private decimal SubtotalOrder(Order? order, Invoice invoice)
    {
        decimal subtotal = 0;

        foreach (var item in order.Items)
        {
            if (item.Quantity <= 0)
            {
                invoice.Warnings.Add($"Invalid quantity for item: {item.Name}");
                continue;
            }

            if (item.Price < 0)
            {
                invoice.Warnings.Add($"Invalid price for item: {item.Name}");
                continue;
            }

            // calculate item subtotal
            decimal totalItemPrice = item.Price * item.Quantity;
            subtotal += totalItemPrice;

            invoice.Items.Add(new InvoiceItem
            {
                Name = item.Name,
                Quantity = item.Quantity,
                Price = item.Price,
                Total = totalItemPrice
            });
        }

        return subtotal;
    }

    private void Validate(Order order)
    {
        // can't be null
        if (order == null)
        {
            throw new InvalidOrderException("Order is null");
        }

        // check if customer name is missing
        if (string.IsNullOrEmpty(order.CustomerName))
        {
            throw new InvalidOrderException("Customer name is missing");
        }

        // check if order is empty
        if (order.Items.Count == 0)
        {
            throw new InvalidOrderException("No items in order");
        }
    }
}

public class InvalidOrderException : Exception
{
    public InvalidOrderException(string message): base(message)
    {

    }
}

// Order and Items
public class Order
{
    public string? CustomerName { get; init; }
    public List<OrderItem>? Items { get; init; }
}

public class OrderItem
{
    public string? Name { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}

// Invoice Classes
public class Invoice
{
    public string? CustomerName { get; set; }
    public List<InvoiceItem>? Items { get; init; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Shipping { get; set; }
    public decimal Total { get; set; }
    public List<string>? Warnings { get; init; }
}

public class InvoiceItem
{
    public string? Name { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public decimal Total { get; set; }
}
