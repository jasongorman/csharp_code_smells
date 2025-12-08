using FeatureEnvy.Model;

namespace FeatureEnvy.Services;

public class BasketService
{
    public void AddToBasket(Basket basket, Product product, int qty)
    {
        var existing = basket.Items.FirstOrDefault(i => i.Product == product);

        if (existing == null)
        {
            basket.Items.Add(new Item
            {
                Product = product,
                Quantity = qty
            });
        }
        else
        {
            existing.Quantity += qty;
        }
    }
}
