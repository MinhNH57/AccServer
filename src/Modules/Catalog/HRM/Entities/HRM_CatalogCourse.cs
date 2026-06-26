using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogCourse
{
    public Guid? Id { get; set; }
    //public int IdAsc { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public string? Description { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? ModifyBy { get; set; }
    public bool? IsActive { get; set; }
    public string? Type { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? CodeUnit { get; set; }

    public string? NameSource { get; set; }        
    public string? CodeSource { get; set; }       

    public string? NameObjective { get; set; }    
    public string? CodeObjective { get; set; }  

    public string? CodeCategory { get; set; } 
    public string? NameCategory { get; set; }      // NVARCHAR(100) NULL

    public string? CodeSkills { get; set; }        // VARCHAR(20) NULL
    public string? NameSkill { get; set; }         // NVARCHAR(100)

    public int? CourseDays { get; set; }           // INT
    public int? CourseHours { get; set; }          // INT
    public int? CourseMinutes { get; set; }        // INT

    public int? MaxDays { get; set; }              // INT
    public int? MaxHours { get; set; }             // INT
    public int? MaxMinutes { get; set; }           // INT

    public bool? MustFinishQuestions { get; set; } // BIT
    public bool? IsMandatory { get; set; }         // BIT
    public bool? ShowComments { get; set; }        // BIT
    public bool? SaveAsTemplate { get; set; }
    public string? BannerUrl { get; set; }        // NVARCHAR(500) NULL
    public string? Author { get; set; }

    public string? ExamRule { get; set; }
    public string? TestType { get; set; }
    public string? ExamType { get; set; }
    public string? Location { get; set; }
    public string? CodeGrp { get; set; }
    public string? NameGrp { get; set; }

}
