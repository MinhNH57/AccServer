using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.QueryModels;

public class QueryCodeCatalogByGroup
{
    [MaxLength(50)] public string TableName { get; set; }
    [MaxLength(10)] public string GroupCode { get; set; }
    public int Length { get; set; }
}