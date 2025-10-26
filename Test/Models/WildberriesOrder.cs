using System.Text.Json.Serialization;

namespace Test.Models;

public class WildberriesOrder
{
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("lastChangeDate")]
    public DateTime LastChangeDate { get; set; }

    [JsonPropertyName("warehouseName")]
    public string WarehouseName { get; set; } = string.Empty;

    [JsonPropertyName("warehouseType")]
    public string WarehouseType { get; set; } = string.Empty;

    [JsonPropertyName("countryName")]
    public string CountryName { get; set; } = string.Empty;

    [JsonPropertyName("oblastOkrugName")]
    public string OblastOkrugName { get; set; } = string.Empty;

    [JsonPropertyName("regionName")]
    public string RegionName { get; set; } = string.Empty;

    [JsonPropertyName("supplierArticle")]
    public string SupplierArticle { get; set; } = string.Empty;

    [JsonPropertyName("nmId")]
    public long NmId { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("techSize")]
    public string TechSize { get; set; } = string.Empty;

    [JsonPropertyName("incomeID")]
    public long IncomeID { get; set; }

    [JsonPropertyName("isSupply")]
    public bool IsSupply { get; set; }

    [JsonPropertyName("isRealization")]
    public bool IsRealization { get; set; }

    [JsonPropertyName("totalPrice")]
    public decimal TotalPrice { get; set; }

    [JsonPropertyName("discountPercent")]
    public int DiscountPercent { get; set; }

    [JsonPropertyName("spp")]
    public int Spp { get; set; }

    [JsonPropertyName("finishedPrice")]
    public decimal FinishedPrice { get; set; }

    [JsonPropertyName("priceWithDisc")]
    public decimal PriceWithDisc { get; set; }

    [JsonPropertyName("isCancel")]
    public bool IsCancel { get; set; }

    [JsonPropertyName("cancelDate")]
    public DateTime CancelDate { get; set; }

    [JsonPropertyName("sticker")]
    public string Sticker { get; set; } = string.Empty;

    [JsonPropertyName("gNumber")]
    public string GNumber { get; set; } = string.Empty;

    [JsonPropertyName("srid")]
    public string Srid { get; set; } = string.Empty;
}

