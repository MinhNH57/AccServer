using System.ComponentModel.DataAnnotations;

namespace Catalog.Base.GenarateCode.Models;

public class QueryCodeCatalogByGroup
{
    [MaxLength(50)] public string TableName { get; set; }
    [MaxLength(10)] public string GroupCode { get; set; }
    public int Length { get; set; }
    public bool Ct { get; set; } = false;
}