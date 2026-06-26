using Newtonsoft.Json;

namespace SmartAccCloud.Application.StoreViewModels;

public class VietQrTaxCode
{
    public class ReponseVietQr
    {
        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("desc")] public string Desc { get; set; }

        [JsonProperty("data")] public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("internationalName")] public string InternationalName { get; set; }

        [JsonProperty("shortName")] public string ShortName { get; set; }

        [JsonProperty("address")] public string Address { get; set; }
    }
}