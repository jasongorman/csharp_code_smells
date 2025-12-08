namespace FeatureEnvy.Model;

public class ShippingNote
{
    public Order Order { get; set; }
    public string ShippingZone { get; set; }
}