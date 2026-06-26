using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.BankAccountForObj;

public class BankAccountForObjectDto
{
    [Required(ErrorMessage = "Số tài khoản không được để trống")]
    public string BankNumbers { get; set; }

    public string? BankCode { get; set; }

    [Required(ErrorMessage = "Tên ngân hàng không được để trống")]
    public string? BankName { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();
    public int IdAsc { get; set; }
    public int? CodeUnit { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsUsing { get; set; } = false;
    public string ObjectCode { get; set; }
}