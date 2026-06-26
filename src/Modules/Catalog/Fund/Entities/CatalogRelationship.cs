using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Fund.Entities;

public class CatalogRelationship
{
    public string? MemberCode {get;set;}
    public string? NameObject {get;set;}
    public string? CitizenIDNumber {get;set;}
    public DateTime? DateOfBirth {get;set;}
    public string? MyRelationship {get;set;}
    public string? SexRelationship {get;set;}
    public string? JobRelationship {get;set;}
    public bool EconomicAutonomy {get;set;}
    public Guid Id {get;set;}
    public Guid IdMember {get;set;}
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc {get;set;}
    public int? CodeUnit {get;set;}
    public string? Notes {get;set;}
    public bool IsActive {get;set;}
}