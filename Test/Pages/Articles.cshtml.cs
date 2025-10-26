using Microsoft.AspNetCore.Mvc.RazorPages;
using Test.Models;
using Test.Services;

namespace Test.Pages;

public class ArticlesModel : PageModel
{
    private readonly IWildberriesService _wildberriesService;

    public ArticlesModel(IWildberriesService wildberriesService)
    {
        _wildberriesService = wildberriesService;
    }

    public List<Article> Articles { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync(bool load = false)
    {
        if (load)
        {
            try
            {
                Articles = await _wildberriesService.GetArticlesAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            }
        }
    }
}

