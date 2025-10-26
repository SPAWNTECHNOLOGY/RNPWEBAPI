namespace Test.Models;

public class Unit
{
    public string SupplierArticle { get; set; } = string.Empty;
    public string WbArticle { get; set; } = string.Empty;
    public string SellerCategory { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    
    // Цены и скидки
    public decimal PriceBeforeDiscount { get; set; }
    public int DiscountPercent { get; set; }
    
    // Выкуп
    public decimal BuyoutPercent { get; set; }
    
    // Удержания WB
    public decimal LitrageByStocks { get; set; }
    public decimal LitrageByCharacteristics { get; set; }
}
