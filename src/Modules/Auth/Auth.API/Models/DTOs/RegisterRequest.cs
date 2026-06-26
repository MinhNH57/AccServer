using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs;

public class RegisterRequest
{
    public string AppCode { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên đơn vị bạn làm việc")]
    public string CompanyName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã số thuế")]
    [RegularExpression(@"^\d{10}(-\d{3})?$", ErrorMessage = "Định dạng mã số thuế không đúng")]
    public string TaxCode { get; set; }

    [Required(ErrorMessage = "Bạn chưa chọn sản phẩm")]
    public List<string> AppName { get; set; }

    public string IDNumber { get; set; }

    public int BusinessType { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ và đệm")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Định dạng email không đúng")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Định dạng số điện thoại không đúng")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn vị trí công việc")]
    public string JobPosition { get; set; }

    public string SMARTSalerCode { get; set; }

    public string Purpose { get; set; }

    public string Query { get; set; }

    public string ReturnURL { get; set; }

    public string QueryParam { get; set; }
}
