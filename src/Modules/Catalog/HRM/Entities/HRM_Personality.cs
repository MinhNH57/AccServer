using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_Personality
{
    public Guid? Id { get; set; }
    public bool? IsAboutYourself { get; set; }
    public string? CodeObj { get; set; }
    public string? HumanResourcesRelations { get; set; }
    public string? PersonalityName { get; set; }
    public int? BirthYear { get; set; }
    public string? Hometown { get; set; }
    public string? Job { get; set; }
    public string? WorkUnit { get; set; }
    public string? Address { get; set; }
}
