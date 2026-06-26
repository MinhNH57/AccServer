using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.Base.Entities.Shifts;
public class CalculateMerit
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(20)]
    public string? CalculateCode { get; set; }

    [StringLength(20)]
    public string? CodeRoom { get; set; }

    [StringLength(20)]
    public string? CodeUnit { get; set; }

    [StringLength(20)]
    public string? DataType { get; set; }

    public double? BeginMerit { get; set; }

    public double? EndMerit { get; set; }

    public double? BeginHours { get; set; }

    public double? EndHours { get; set; }
    public double? EatWorkShift { get; set; }

    [StringLength(50)]
    public string? SalaryType { get; set; }

    [StringLength(50)]
    public string? AppyFor { get; set; }

    public bool? AllWeek { get;set;}
    public bool? Monday { get;set;}
    public bool? Tuesday { get; set; }
    public bool? Wednesday { get; set; }
    public bool? Thursday { get; set; }
    public bool? Friday { get; set; }
    public bool? Saturday { get; set; }
    public bool? Sunday { get; set; }
    public string? CodeShiftsAdvanced { get; set; }
}
