using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.Models;
using Test.Services;

namespace Test.Pages;

public class UnitModel : PageModel
{
    private readonly IWildberriesService _wildberriesService;

    public UnitModel(IWildberriesService wildberriesService)
    {
        _wildberriesService = wildberriesService;
    }

    public List<Unit> Units { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync(bool load = false)
    {
        // Загружаем данные только если пользователь нажал кнопку
        if (load)
        {
            try
            {
                Units = await _wildberriesService.GetUnitsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            }
        }
    }
}
