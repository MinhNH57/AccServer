using System.ComponentModel.DataAnnotations;

namespace Catalog.Fund.Models;

public class ObjectViewMobile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Không được để trống tên đơn vị")]
    public string? ObjName { get; set; }
    [Required(ErrorMessage = "Không được để trống căn CCCD")]
    public string? CitizenIDNumber { get; set; }
    [Required(ErrorMessage = "Không được để trống ngày cấp")]
    public DateTime? RangeDate { get; set; }
    [Required(ErrorMessage = "Không được để trống ngày sinh")]
    public DateTime? DateOfBirth { get; set; }
    public decimal? DebitBalance { get; set; }
    public string? ObjJob { get; set; }
    public string? ObjAddress { get; set; }
    public string? BankName { get; set; }
    public string? AccountNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ObjSex { get; set; }
    public string? CreateBy { get; set; }
    public string? DataType { get; set; }

}