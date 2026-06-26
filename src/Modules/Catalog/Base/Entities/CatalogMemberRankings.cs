using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogMemberRankings
{
    [Key]
    [Required(ErrorMessage = "Mã xếp hạng không được để trống")]
    public string MemberRankingsCode { get; set; }
    public string? MemberRankingsName { get; set; }
    public string? ServicesCode { get; set; }
    public string? ServicesName { get; set; }
    [Precision(16, 2)]
    public decimal? StartNumber { get; set; }
    [Precision(16, 2)]
    public decimal? EndNumber { get; set; }
    public string? Notes { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}
