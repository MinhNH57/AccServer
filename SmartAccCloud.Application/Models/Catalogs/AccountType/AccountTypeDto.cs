using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAccCloud.Application.Models.Catalogs.AccountType;

public class AccountTypeDto
{
    [Key]
    [Required(ErrorMessage = "Mã loại không được để trống")]
    public string AccountTypeCode { get; set; }

    [Required(ErrorMessage = "Tên loại khoản không được để trống")]
    public string AccountTypeName { get; set; }

    public string? Notes { get; set; }
    [NotMapped] public Guid Id { get; set; }
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
}