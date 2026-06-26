using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Base.Entities;
public class CatalogBankAccountForObject
{
    [Required(ErrorMessage = "Số tài khoản không được để trống")]
    public string BankNumbers { get; set; } = null!;
    public string? BankCode { get; set; }
    public string? SwiftCode { get; set; }
    [Required(ErrorMessage = "Tên ngân hàng không được để trống")]
    public string? BankName { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int IdAsc { get; set; } 
    public int? CodeUnit { get; set; } = 100;
    public bool IsActive { get; set; }
    public bool IsUsing { get; set; }
    public string ObjectCode { get; set; } = null!;
    public string? AddressBank { get; set; } = "";
}