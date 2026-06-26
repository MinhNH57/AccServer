using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Domain;

public class DataSelect
{
    [Key]
    public Guid Id {get;set;}
    public int? CodeUnit {get;set;}
    public string? TableName {get;set;}
    public string? TypeDocument {get;set;}
    public string? ListFieldName {get;set;}
    public bool BeginAndEndDate {get;set;}
    public string? FileLayout {get;set;}
    public string? Caption {get;set;}
    public string? Comfirm {get;set;}
    public string? GroupByField {get;set;}
    public string? OrderByField {get;set;}
    public double? FormWeight {get;set;}
    public double? FormHight {get;set;}
    public string? StoreName {get;set;}
    public int? NumberRow {get;set;}}