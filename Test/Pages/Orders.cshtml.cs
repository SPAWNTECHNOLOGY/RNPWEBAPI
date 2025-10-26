using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.Models;
using Test.Services;

namespace Test.Pages;

public class OrdersModel : PageModel
{
    private readonly IWildberriesService _wildberriesService;

    public OrdersModel(IWildberriesService wildberriesService)
    {
        _wildberriesService = wildberriesService;
    }

    public List<WildberriesOrder> Orders { get; set; } = new();
    public DateTime DateFrom { get; set; } = DateTime.Now.AddMonths(-1);
    public string? ErrorMessage { get; set; }

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
                Orders = await _wildberriesService.GetOrdersAsync(DateFrom);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            }
        }
    }
}

