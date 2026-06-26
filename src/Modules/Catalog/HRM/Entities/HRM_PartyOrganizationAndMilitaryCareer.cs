using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_PartyOrganizationAndMilitaryCareer
{
    public Guid? Id { get; set; }
    public string? CodeObj { get; set; }
    public string? PartyUnionTitle { get; set; }
    public string? CodePoliticalTheory { get; set; }
    public string? LevelOfPoliticalTheory { get; set; }
    public DateTime? DateOfJoiningTheParty { get; set; }
    public DateTime? OfficialPartyMembershipDate { get; set; }
    public DateTime? EnlistmentDate { get; set; }
    public DateTime? DemobilizationDate { get; set; }
    public bool? IsMartyrsFamily { get; set; }
    public string? TypeOfWoundedSoldier { get; set; }
}
