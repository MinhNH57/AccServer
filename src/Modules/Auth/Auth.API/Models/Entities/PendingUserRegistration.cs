using System;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.Entities;

public class PendingUserRegistration
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(50)]
    public string TaxCode { get; set; }

    [Required]
    [MaxLength(500)]
    public string AppName { get; set; }

    [MaxLength(500)]
    public string AppCode { get; set; }

    [MaxLength(50)]
    public string IDNumber { get; set; }

    public int? BusinessType { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string Mobile { get; set; }

    [Required]
    [MaxLength(100)]
    public string JobPosition { get; set; }

    [MaxLength(50)]
    public string SMARTSalerCode { get; set; }

    [MaxLength(500)]
    public string Purpose { get; set; }

    [MaxLength(1000)]
    public string Query { get; set; }

    [MaxLength(500)]
    public string ReturnURL { get; set; }

    [MaxLength(500)]
    public string QueryParam { get; set; }

    public int Status { get; set; } = 0; // 0 = Mới, 1 = Đang xử lý, 2 = Đã duyệt, 3 = Từ chối

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime? Processed { get; set; }

    [MaxLength(100)]
    public string ProcessedBy { get; set; }

    [MaxLength(1000)]
    public string Note { get; set; }
}
