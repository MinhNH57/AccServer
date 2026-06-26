using Newtonsoft.Json;

namespace AddOn.TonMyAnh.Mobile.BalanceFluctuation.Models;

public class CreateBalanceFluctuationRequest
{
    [JsonProperty(PropertyName = "codeUnit")]
    public int CodeUnit { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; set; }

    [JsonProperty(PropertyName = "amountBlance")]
    public int AmountBlance { get; set; }

    [JsonProperty(PropertyName = "fullContent")]
    public string? FullContent { get; set; }

    [JsonProperty(PropertyName = "remittanceContent")]
    public string? RemittanceContent { get; set; }

    [JsonProperty(PropertyName = "bankOfAccount")]
    public string? BankOfAccount { get; set; }

    [JsonProperty(PropertyName = "bankOfName")]
    public string? BankOfName { get; set; }

    [JsonProperty(PropertyName = "bankOfAccountReceive")]
    public string? BankOfAccountReceive { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty(PropertyName = "notes")]
    public string? Notes { get; set; }
}