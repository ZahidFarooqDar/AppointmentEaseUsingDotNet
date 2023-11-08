using Newtonsoft.Json;

namespace EcommereAPI.Helpers
{
    public class Product
    {
        [JsonProperty("id")]
        public string ProductId { get; set; } = "unique_product_id";

        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("input_type")]
        public string InputType { get; set; }

        [JsonProperty("api")]
        public string SKU { get; set; }

        [JsonProperty("length")]
        public double? LengthInCm { get; set; }

        [JsonProperty("width")]
        public double? WidthInCm { get; set; }

        [JsonProperty("height")]
        public double? HeightInCm { get; set; }

        [JsonProperty("weight")]
        public double? WeightInKg { get; set; }

        [JsonProperty("item_category_id")]
        public int ItemCategoryId { get; set; }

        [JsonProperty("platform_product_id")]
        public string PlatformProductId { get; set; }

        [JsonProperty("cost_price")]
        public double? CostPrice { get; set; }

        [JsonProperty("cost_price_currency")]
        public string CostPriceCurrency { get; set; }

        [JsonProperty("selling_price")]
        public double? SellingPrice { get; set; }

        [JsonProperty("selling_price_currency")]
        public string SellingPriceCurrency { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("pick_location")]
        public string PickLocation { get; set; }

        [JsonProperty("origin_country_alpha2")]
        public string OriginCountryAlpha2 { get; set; }

        [JsonProperty("hs_code")]
        public string HSCode { get; set; }

        [JsonProperty("contains_liquids")]
        public bool? ContainsLiquids { get; set; }

        [JsonProperty("contains_battery_pi966")]
        public bool? ContainsBatteryPI966 { get; set; }

        [JsonProperty("contains_battery_pi967")]
        public bool? ContainsBatteryPI967 { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
