using Microsoft.EntityFrameworkCore;

namespace Catalog.SGas.Entities;
public class ProjectManagementExpenses
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? CodeUnit { get; set; }
    public string CodeExpenses { get; set; }
    public string NameExpenses { get; set; }
    public string CodeProject { get; set; }
    public string NameProject { get; set; }
    [Precision(18,0)]
    public decimal Money { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}
