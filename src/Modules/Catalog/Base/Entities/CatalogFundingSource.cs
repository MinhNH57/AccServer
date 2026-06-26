using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogFundingSource
{
    [Key]
    [Required(ErrorMessage = "Mã nguồn kinh phí không được để trống")]
    
    public string FundingSourceCode { get; set; } = null!;
    [Required(ErrorMessage = "Tên nguồn kinh phí không được để trống")]
    public string FundingSourceName { get; set; }
    public string? Notes { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
}
