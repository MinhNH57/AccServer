using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Fund.Enums;

namespace Catalog.Fund.Models;

public class CatalogRelationshipDto
{
    public string? MemberCode { get; set; }
    public string? NameObject { get; set; }
    public string? CitizenIDNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? MyRelationship { get; set; }
    public string? SexRelationship { get; set; }
    public string? JobRelationship { get; set; }
    public bool EconomicAutonomy { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdMember { get; set; }
    [NotMapped]
    public StatusModel? Type { get; set; }
}