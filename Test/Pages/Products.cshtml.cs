using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.Models;
using Test.Services;

namespace Test.Pages;

public class ProductsModel : PageModel
{
    private readonly IWildberriesService _wildberriesService;

    public ProductsModel(IWildberriesService wildberriesService)
    {
        _wildberriesService = wildberriesService;
    }

    public DateTime DateFrom { get; set; } = DateTime.Now.AddMonths(-1);
    public string? ErrorMessage { get; set; }
    public List<ProductSummary> Products { get; set; } = new();

    public async Task OnGetAsync(DateTime? dateFrom = null, bool load = false)
    {
        if (dateFrom.HasValue)
        {
            DateFrom = dateFrom.Value;
        }

        if (!load)
        {
            return;
        }

        try
        {
            var orders = await _wildberriesService.GetOrdersAsync(DateFrom);
            Products = orders
                .GroupBy(o => new { o.SupplierArticle, o.Subject, o.Brand })
                .Select(g => new ProductSummary
                {
                    Article = g.Key.SupplierArticle,
                    Subject = g.Key.Subject,
                    Brand = g.Key.Brand,
                    OrdersCount = g.Count(),
                    TotalRevenue = g.Sum(x => x.FinishedPrice),
                    AvgDiscount = g.Average(x => x.DiscountPercent)
                })
                .OrderByDescending(p => p.TotalRevenue)
                .ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            Products = new List<ProductSummary>();
        }
    }
}

public class ProductSummary
{
    public string Article { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
    public decimal TotalRevenue { get; set; }
    public double AvgDiscount { get; set; }
}
