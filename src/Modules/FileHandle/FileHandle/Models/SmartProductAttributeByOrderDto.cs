using System.ComponentModel.DataAnnotations.Schema;

namespace FileHandle.Models;

public class SmartProductAttributeByOrderDto
{
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? AttributeCode { get; set; }
    public string? AttributeName { get; set; }
    public string? CodeSize { get; set; }
    public string? NameSize { get; set; }
    public double? Quantity { get; set; }
    public string? Notes { get; set; }
    public string? DataType { get; set; }
    public string? TypeAttribute { get; set; }
    public bool IsActive { get; set; }
    public int? CodeUnit { get; set; }
    public string? ManufacturingStageBelong { get; set; }
    public string? ManufacturingStageBelongName { get; set; }
}
