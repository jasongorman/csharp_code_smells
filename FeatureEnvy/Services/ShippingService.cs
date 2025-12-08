using FeatureEnvy.Model;

namespace FeatureEnvy.Services;

public class ShippingService
{
    public ShippingNote CreateShippingNote(Order order)
    {
        string zone;

        if (order.ShippingAddress.Country == "UK")
            zone = "UK";
        else if (order.ShippingAddress.Country == "DE" ||
                 order.ShippingAddress.Country == "FR")
            zone = "EU";
        else
            zone = "Other";

        return new ShippingNote
        {
            Order = order,
            ShippingZone = zone
        };
    }
}
