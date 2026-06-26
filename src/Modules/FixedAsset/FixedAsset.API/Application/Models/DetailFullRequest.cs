namespace FixedAsset.API.Application.Models;

public class DetailFullRequest
{
    public string Type { get; set; }
    public string View { get; set; } 
    public string Key { get; set; } 
    public int RefType { get; set; }
    public int RefTypeCategory { get; set; } 
    public List<DetailFullDetail> Details { get; set; } = [];
}

public class DetailFullDetail
{
    public string Type { get; set; }
    public string Alias { get; set; }
}