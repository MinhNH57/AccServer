namespace Supply.API.Application.Models;

public class DetailFullRequest
{
    public string Type { get; set; }
    public string View { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int RefType { get; set; } = 0;
    public int RefTypeCategory { get; set; } = 0;
    public List<DetailFullDetail> Details { get; set; } = [];
}

public class DetailFullDetail
{
    public string Type { get; set; }
    public string Alias { get; set; }
}
