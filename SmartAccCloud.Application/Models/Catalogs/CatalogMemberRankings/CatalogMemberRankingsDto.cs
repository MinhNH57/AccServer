using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogMemberRankings;

public class CatalogMemberRankingsDto
{
    [Required(ErrorMessage = "Mã xếp hạng không được để trống")]
    public string MemberRankingsCode { get; set; }

    public string? MemberRankingsName { get; set; }
    public string? ServicesCode { get; set; }
    public string? ServicesName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Notes { get; set; }
}