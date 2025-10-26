using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.Models;
using Test.Services;

namespace Test.Pages;

public class StocksModel : PageModel
{
    private readonly IWildberriesService _wildberriesService;

    public StocksModel(IWildberriesService wildberriesService)
    {
        _wildberriesService = wildberriesService;
    }

    public List<WildberriesStock> Stocks { get; set; } = new();
    public DateTime DateFrom { get; set; } = DateTime.Now.AddDays(-7);
    public string? ErrorMessage { get; set; }

    public List<GroupedStock> GroupedStocks => Stocks
        .GroupBy(s => new { s.SupplierArticle, s.Subject, s.Brand, s.TechSize })
        .Select(g => new GroupedStock
        {
            Article = g.Key.SupplierArticle,
            Subject = g.Key.Subject,
            Brand = g.Key.Brand,
            Size = g.Key.TechSize,
            TotalQuantity = g.Sum(s => s.Quantity),
            InWayToClient = g.Sum(s => s.InWayToClient),
            InWayFromClient = g.Sum(s => s.InWayFromClient),
            Warehouses = g.Count(),
            WarehouseList = string.Join(", ", g.Select(s => $"{s.WarehouseName} ({s.Quantity})"))
        })
        .OrderByDescending(g => g.TotalQuantity)
        .ToList();

    public async Task OnGetAsync(DateTime? dateFrom = null, bool load = false)
    {
        if (dateFrom.HasValue)
        {
            DateFrom = dateFrom.Value;
        }

        // Загружаем данные только если пользователь нажал кнопку
        if (load)
        {
            try
            {
                Stocks = await _wildberriesService.GetStocksAsync(DateFrom);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            }
        }
    }
}

public class GroupedStock
{
    public string Article { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public int TotalQuantity { get; set; }
    public int InWayToClient { get; set; }
    public int InWayFromClient { get; set; }
    public int Warehouses { get; set; }
    public string WarehouseList { get; set; } = string.Empty;
}

