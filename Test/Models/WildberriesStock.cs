using System.Text.Json.Serialization;

namespace Test.Models;

public class WildberriesStock
{
    [JsonPropertyName("lastChangeDate")]
    public DateTime LastChangeDate { get; set; }

    [JsonPropertyName("warehouseName")]
    public string WarehouseName { get; set; } = string.Empty;

    [JsonPropertyName("supplierArticle")]
    public string SupplierArticle { get; set; } = string.Empty;

    [JsonPropertyName("nmId")]
    public long NmId { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("inWayToClient")]
    public int InWayToClient { get; set; }

    [JsonPropertyName("inWayFromClient")]
    public int InWayFromClient { get; set; }

    [JsonPropertyName("quantityFull")]
    public int QuantityFull { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("techSize")]
    public string TechSize { get; set; } = string.Empty;

    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    [JsonPropertyName("Discount")]
    public int Discount { get; set; }

    [JsonPropertyName("isSupply")]
    public bool IsSupply { get; set; }

    [JsonPropertyName("isRealization")]
    public bool IsRealization { get; set; }

    [JsonPropertyName("SCCode")]
    public string SCCode { get; set; } = string.Empty;
}

