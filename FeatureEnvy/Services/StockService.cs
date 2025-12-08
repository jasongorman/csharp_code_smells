using FeatureEnvy.Model;

namespace FeatureEnvy.Services;

public class StockService
{
    private readonly List<WarehouseStock> _stocks;

    public StockService(List<WarehouseStock> stocks)
    {
        _stocks = stocks;
    }

    public bool CheckStock(Product product, int qty)
    {
        var stock = _stocks.FirstOrDefault(s => s.Product == product);
        return stock != null && stock.Quantity >= qty;
    }
}