using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.Models;
using Test.Services;

namespace Test.Pages;

public class ProductModel : PageModel
{
    private readonly IWildberriesService _wildberriesService;

    public ProductModel(IWildberriesService wildberriesService)
    {
        _wildberriesService = wildberriesService;
    }

    [BindProperty(SupportsGet = true)]
    public string Article { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public DateTime DateFrom { get; set; } = DateTime.Now.AddMonths(-1);

    public string? ErrorMessage { get; set; }

    public List<WildberriesOrder> Orders { get; set; } = new();

    public int OrdersCount => Orders.Count;
    public decimal TotalRevenue => Orders.Sum(o => o.FinishedPrice);
    public double AvgDiscount => Orders.Any() ? Orders.Average(o => o.DiscountPercent) : 0;

    public async Task OnGetAsync(bool load = false)
    {
        if (!load || string.IsNullOrWhiteSpace(Article))
        {
            return;
        }

        try
        {
            var orders = await _wildberriesService.GetOrdersAsync(DateFrom);
            Orders = orders.Where(o => o.SupplierArticle.Equals(Article, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            Orders = new List<WildberriesOrder>();
        }
    }
}
