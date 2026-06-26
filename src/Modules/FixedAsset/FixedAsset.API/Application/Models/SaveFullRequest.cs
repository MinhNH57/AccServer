using System.Text.Json;

namespace FixedAsset.API.Application.Models;

public class SaveFullRequest
{
    public string Type { get; set; }
    public string View { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int RefType { get; set; } = 0;
    public int RefTypeCategory { get; set; } = 0;
    public List<SaveFullDetail> Details { get; set; } = [];
    public string PostStatus { get; set; }
    public JsonElement Object { get; set; }
    public AuditingLog AuditingLog { get; set; }
}

public class SaveFullDetail
{
    public string Type { get; set; }
    public string Alias { get; set; }
    public List<JsonElement> Objects { get; set; } = [];
}
