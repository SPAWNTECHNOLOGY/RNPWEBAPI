namespace Test.Models;

public class Article
{
    // Основная информация
    public string SupplierArticle { get; set; } = string.Empty;
    public string WbArticle { get; set; } = string.Empty;
    
    // Заказы
    public int TotalOrders { get; set; }
    public int FactOrdersFBW { get; set; }
    public int FactOrdersFBS { get; set; }
    public int FactOrdersDBS_DBW { get; set; }
    public int PlanOrders { get; set; }
    public decimal FPPercent { get; set; }
    public decimal OrdersSum { get; set; }
    
    // Выкуп
    public int FactRedemption { get; set; }
    public decimal RedemptionSum { get; set; }
    
    // Раздачи и самовыкупы
    public int Giveaways { get; set; }
    public decimal LossGiveaways { get; set; }
    public int Blog { get; set; }
    public decimal LossBlog { get; set; }
    public int SMBK { get; set; }
    public decimal LossSMBK { get; set; }
    
    // Остатки
    public int StockIndex { get; set; }
    public decimal Turnover { get; set; }
    
    // Цены
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public decimal PriceWithoutSPP { get; set; }
    public decimal SPP { get; set; }
    public decimal PriceWithSPP { get; set; }
    
    // Выкуп процент
    public decimal RedemptionPercent { get; set; }
    
    // OV (выкуп)
    public int OV_Pcs { get; set; }
    public decimal OV_Rub { get; set; }
    
    // ДРР
    public decimal DRR_Total { get; set; }
    public decimal DRR_PRK { get; set; }
    public decimal DRR_ARK { get; set; }
    public decimal DRR_ZKZ_Percent { get; set; }
    public decimal DRR_OV_Percent { get; set; }
    public decimal DRR_OP_Percent { get; set; }
    
    // OP (операционная прибыль)
    public decimal OP_OV_WithoutDRR { get; set; }
    public decimal OP_OV_WithDRR { get; set; }
    
    // На единицу
    public decimal DRR_PerUnit { get; set; }
    public decimal OP_PerUnit_WithDRR { get; set; }
    public decimal Margin_WithDRR { get; set; }
    public decimal ROI_WithDRR { get; set; }
    public decimal OP_PerUnit_WithoutDRR { get; set; }
    public decimal Margin_WithoutDRR { get; set; }
    public decimal ROI_WithoutDRR { get; set; }
    
    // Рекламные метрики - общие
    public int TotalImpressions { get; set; }
    public decimal TotalCTR { get; set; }
    public int TotalClicks { get; set; }
    public decimal TotalCR_Cart { get; set; }
    public int TotalCart { get; set; }
    public int TotalFavorites { get; set; }
    public decimal TotalCR_Order { get; set; }
    public int TotalOrders_Ad { get; set; }
    
    // Рекламные метрики - органические
    public int OrganicImpressions { get; set; }
    public decimal OrganicCTR { get; set; }
    public int OrganicClicks { get; set; }
    public decimal OrganicCR_Cart { get; set; }
    public int OrganicCart { get; set; }
    public decimal OrganicCR_Order { get; set; }
    public int OrganicOrders { get; set; }
    
    // Рекламные метрики - срзн (суммарные)
    public int SummaryImpressions { get; set; }
    public decimal SummaryCTR { get; set; }
    public int SummaryClicks { get; set; }
    public decimal SummaryCR_Cart { get; set; }
    public int SummaryCart { get; set; }
    public decimal SummaryCR_Order { get; set; }
    public int SummaryOrders { get; set; }
    
    // Стоимости срзн
    public decimal SummaryCPC { get; set; }
    public decimal SummaryCPL { get; set; }
    public decimal SummaryCPO { get; set; }
    public decimal SummaryCPM { get; set; }
    
    // ROMI
    public decimal ROMI_PK { get; set; }
    
    // Рекламные метрики - прк
    public int PrkImpressions { get; set; }
    public decimal PrkCTR { get; set; }
    public int PrkClicks { get; set; }
    public decimal PrkCR_Cart { get; set; }
    public int PrkCart { get; set; }
    public decimal PrkCR_Order { get; set; }
    public int PrkOrders { get; set; }
    public decimal PrkDRR { get; set; }
    
    // Стоимости прк
    public decimal PrkCPC { get; set; }
    public decimal PrkCPL { get; set; }
    public decimal PrkCPO { get; set; }
    public decimal PrkCPM { get; set; }
    
    // Рекламные метрики - арк
    public int ArkImpressions { get; set; }
    public decimal ArkCTR { get; set; }
    public int ArkClicks { get; set; }
    public decimal ArkCR_Cart { get; set; }
    public int ArkCart { get; set; }
    public decimal ArkCR_Order { get; set; }
    public int ArkOrders { get; set; }
    public decimal ArkDRR { get; set; }
    
    // Стоимости арк
    public decimal ArkCPC { get; set; }
    public decimal ArkCPL { get; set; }
    public decimal ArkCPO { get; set; }
    public decimal ArkCPM { get; set; }
    
    // Склады FBW
    public int FBW_Total { get; set; }
    public int FBW_Electrostal { get; set; }
    public int FBW_Koledino { get; set; }
    public int FBW_Podolsk { get; set; }
    public int FBW_Kazan { get; set; }
    public int FBW_Tula { get; set; }
    public int FBW_Nevinnomyssk { get; set; }
    public int FBW_Krasnodar { get; set; }
    public int FBW_Other { get; set; }
    
    // В пути
    public int InTransitToClient { get; set; }
    public int InTransitFromClient { get; set; }
    
    // FBS и локализация
    public int FBS { get; set; }
    public decimal LocalPercent { get; set; }
}
