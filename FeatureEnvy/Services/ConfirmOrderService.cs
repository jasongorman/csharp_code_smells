using FeatureEnvy.Model;

namespace FeatureEnvy.Services;

public class OrderConfirmationService
{
    private readonly StockService _stockService;

    public OrderConfirmationService(StockService stockService)
    {
        _stockService = stockService;
    }

    public bool ConfirmOrder(Order order)
    {
        foreach (var item in order.Items)
        {
            if (!_stockService.CheckStock(item.Product, item.Quantity))
            {
                order.Confirmed = false;
                return false;
            }
        }

        order.Confirmed = true;
        return true;
    }
}
