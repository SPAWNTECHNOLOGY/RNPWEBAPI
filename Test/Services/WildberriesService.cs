using System.Text.Json;
using Test.Models;

namespace Test.Services;

public interface IWildberriesService
{
    Task<List<WildberriesOrder>> GetOrdersAsync(DateTime dateFrom);
    Task<List<WildberriesStock>> GetStocksAsync(DateTime dateFrom);
    Task<List<Unit>> GetUnitsAsync();
    Task<List<Article>> GetArticlesAsync();
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

    public async Task<List<Article>> GetArticlesAsync()
    {
        try
        {
            // Получаем базовые данные
            var orders = await GetOrdersAsync(DateTime.Now.AddDays(-30));
            var stocks = await GetStocksAsync(DateTime.Now.AddDays(-30));
            
            // Группируем по артикулу
            var articleGroups = stocks
                .Where(s => !string.IsNullOrEmpty(s.SupplierArticle))
                .GroupBy(s => s.SupplierArticle)
                .ToList();
            
            var articles = new List<Article>();
            
            foreach (var group in articleGroups)
            {
                var firstStock = group.First();
                var article = new Article
                {
                    SupplierArticle = firstStock.SupplierArticle,
                    WbArticle = firstStock.NmId.ToString(),
                    
                    // Цены (из остатков)
                    Price = firstStock.Price,
                    Discount = firstStock.Discount,
                    
                    // Рассчитываемые цены
                    PriceWithoutSPP = firstStock.Price - (firstStock.Price * firstStock.Discount / 100),
                    
                    // Остатки по складам
                    StockIndex = group.Sum(s => s.Quantity),
                    InTransitToClient = group.Sum(s => s.InWayToClient),
                    InTransitFromClient = group.Sum(s => s.InWayFromClient),
                    
                    // FBW по городам
                    FBW_Electrostal = GetStockByWarehouse(group, "Электросталь"),
                    FBW_Koledino = GetStockByWarehouse(group, "Коледино"),
                    FBW_Podolsk = GetStockByWarehouse(group, "Подольск"),
                    FBW_Kazan = GetStockByWarehouse(group, "Казань"),
                    FBW_Tula = GetStockByWarehouse(group, "Тула"),
                    FBW_Nevinnomyssk = GetStockByWarehouse(group, "Невинномысск"),
                    FBW_Krasnodar = GetStockByWarehouse(group, "Краснодар"),
                };
                
                // FBW Total
                article.FBW_Total = article.FBW_Electrostal + article.FBW_Koledino + 
                                article.FBW_Podolsk + article.FBW_Kazan + 
                                article.FBW_Tula + article.FBW_Nevinnomyssk + 
                                article.FBW_Krasnodar;
                
                // FBS остаток
                article.FBS = group.Sum(s => s.QuantityFull) - article.StockIndex - 
                                article.InTransitToClient - article.InTransitFromClient;
                
                // FBW Other
                article.FBW_Other = article.StockIndex - article.FBW_Total - article.FBS - 
                                    article.InTransitToClient - article.InTransitFromClient;
                
                // Заказы по типам
                var articleOrders = orders.Where(o => o.SupplierArticle == article.SupplierArticle).ToList();
                article.FactOrdersFBW = articleOrders.Count(o => o.WarehouseType.Contains("WB"));
                article.FactOrdersFBS = articleOrders.Count(o => o.WarehouseType.Contains("FBS"));
                article.FactOrdersDBS_DBW = articleOrders.Count(o => o.WarehouseType.Contains("DBS") || o.WarehouseType.Contains("DBW"));
                
                // Общие заказы
                article.TotalOrders = article.FactOrdersFBW + article.FactOrdersFBS + article.FactOrdersDBS_DBW;
                
                // Остальные поля будут заполняться позже через API рекламы
                
                articles.Add(article);
            }
            
            return articles.OrderBy(a => a.SupplierArticle).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching articles: {ex.Message}");
            return new List<Article>();
        }
    }
    
    private int GetStockByWarehouse(IGrouping<string, WildberriesStock> group, string warehouseName)
    {
        var stock = group.FirstOrDefault(s => s.WarehouseName.Contains(warehouseName));
        return stock?.Quantity ?? 0;
    }
}

