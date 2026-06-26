using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.FundingSource;

public class FundingSourceDto
{
    [Key]
    [Required(ErrorMessage = "Mã nguồn kinh phí không được để trống")]

    public string FundingSourceCode { get; set; }

    [Required(ErrorMessage = "Tên nguồn kinh phí không được để trống")]
    public string FundingSourceName { get; set; }

    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; }
}