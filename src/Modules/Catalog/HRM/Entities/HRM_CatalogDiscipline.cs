using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.HRM.Entities;
public class HRM_CatalogDiscipline
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
	//public int? IdAsc { get; set; }
    public string? DisciplineCode { get; set; }
    public string? DisciplineName { get; set; }
    public string? Notes { get; set; }
    public string? Codeunit { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ModufyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool? IsActive { get; set; }
}
