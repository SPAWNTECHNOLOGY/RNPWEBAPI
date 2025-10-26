using System.Text.Json;
using Test.Models;

namespace Test.Services;

public interface IWildberriesService
{
    Task<List<WildberriesOrder>> GetOrdersAsync(DateTime dateFrom);
    Task<List<WildberriesStock>> GetStocksAsync(DateTime dateFrom);
    Task<List<Unit>> GetUnitsAsync();
}

public class WildberriesService : IWildberriesService
{
    private readonly HttpClient _httpClient;
    private const string OrdersApiUrl = "https://statistics-api.wildberries.ru/api/v1/supplier/orders";
    private const string StocksApiUrl = "https://statistics-api.wildberries.ru/api/v1/supplier/stocks";
    private const string UnitsApiUrl = "https://content-api.wildberries.ru/content/v1/cards/cursor/list";
    private const string AuthToken = "eyJhbGciOiJFUzI1NiIsImtpZCI6IjIwMjUwOTA0djEiLCJ0eXAiOiJKV1QifQ.eyJlbnQiOjEsImV4cCI6MTc3NjE5MjM2NiwiaWQiOiIwMTk5ZTE3OC0xMjExLTdjMGYtODBlNi05OWE3NmMzNzI3NzEiLCJpaWQiOjE0MjMzNTI2LCJvaWQiOjQwODIxNzIsInMiOjE1NDg2LCJzaWQiOiIxMjk0ZjVkZi0wNTJmLTQzOTItOWM3ZS0yMzU3ZWI4YzNkNDQiLCJ0IjpmYWxzZSwidWlkIjoxNDIzMzUyNn0.5LUW9PrRQ7EqcAZRv166LPFtNz0-oPzOnlRUoFnXSUEb8GSUcd4GyG7UKQNlDqCE-UX-tR1MeemOFQyL2x4SXw";

    public WildberriesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", AuthToken);
    }

    public async Task<List<WildberriesOrder>> GetOrdersAsync(DateTime dateFrom)
    {
        try
        {
            var dateFromString = dateFrom.ToString("yyyy-MM-ddTHH:mm:ss");
            var requestUri = $"{OrdersApiUrl}?dateFrom={dateFromString}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var orders = JsonSerializer.Deserialize<List<WildberriesOrder>>(content, options);
            return orders ?? new List<WildberriesOrder>();
        }
        catch (Exception ex)
        {
            // В реальном приложении здесь должно быть логирование
            Console.WriteLine($"Error fetching orders: {ex.Message}");
            return new List<WildberriesOrder>();
        }
    }

    public async Task<List<WildberriesStock>> GetStocksAsync(DateTime dateFrom)
    {
        try
        {
            var dateFromString = dateFrom.ToString("yyyy-MM-ddTHH:mm:ss");
            var requestUri = $"{StocksApiUrl}?dateFrom={dateFromString}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var stocks = JsonSerializer.Deserialize<List<WildberriesStock>>(content, options);
            return stocks ?? new List<WildberriesStock>();
        }
        catch (Exception ex)
        {
            // В реальном приложении здесь должно быть логирование
            Console.WriteLine($"Error fetching stocks: {ex.Message}");
            return new List<WildberriesStock>();
        }
    }

    public async Task<List<Unit>> GetUnitsAsync()
    {
        try
        {
            // Получаем данные о товарах для извлечения артикулов
            var stocks = await GetStocksAsync(DateTime.Now.AddMonths(-1));
            
            var units = stocks
                .Where(s => !string.IsNullOrEmpty(s.SupplierArticle))
                .GroupBy(s => s.SupplierArticle)
                .Select(g => 
                {
                    var first = g.First();
                    return new Unit
                    {
                        SupplierArticle = first.SupplierArticle,
                        WbArticle = first.NmId.ToString(),
                        SellerCategory = first.Category,
                        Brand = first.Brand,
                        PriceBeforeDiscount = first.Price,
                        DiscountPercent = first.Discount,
                        BuyoutPercent = 0, // TODO: Получить из API
                        LitrageByStocks = 0, // TODO: Получить из API
                        LitrageByCharacteristics = 0 // TODO: Получить из API
                    };
                })
                .OrderBy(u => u.SupplierArticle)
                .ToList();

            return units;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching units: {ex.Message}");
            return new List<Unit>();
        }
    }
}

