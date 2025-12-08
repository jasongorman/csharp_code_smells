namespace FeatureEnvy.Model;

public class Order
{
    public List<Item> Items { get; set; } = new();
    public Address ShippingAddress { get; set; }
    public decimal Total { get; set; }
    public bool Confirmed { get; set; }
}