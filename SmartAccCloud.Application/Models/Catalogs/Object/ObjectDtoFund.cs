using System.ComponentModel.DataAnnotations;

namespace SmartAccCloud.Application.Models.Catalogs.Object
{
     public class ObjectDtoFund
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? GrpName { get; set; }
        public string? CodeManager { get; set; }
        public string? NameManager { get; set; }
        public string? ObjCode { get; set; }
        [Required(ErrorMessage = "Không được để trống tên đơn vị")]
        public string? ObjName { get; set; }
        public string? Position { get; set; }
        public string? ObjAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? AccountNumber { get; set; }
        public string? BankName { get; set; }
        public decimal? DebitBalance { get; set; }
        public string? Notes { get; set; }

        public string? ObjSex { get; set; }
        public string? ObjJob { get; set; }

        [Required(ErrorMessage = "Không được để trống ngày cấp")]
        public DateTime? RangeDate { get; set; }
        public string? GrantedBy { get; set; }
        public string? MyObjName { get; set; }
        [Required(ErrorMessage = "Không được để trống ngày sinh")]
        public DateTime? MyDOB { get; set; }
        [Required(ErrorMessage = "Không được để trống ngày cấp")]
        public DateTime? MyRangeDate { get; set; }
        [Required(ErrorMessage = "Không được để trống CCCD người thừa kế")]
        public string? MyCitizenID { get; set; }
        public string? MyGrantedBy { get; set; }
        public string? MyObjAddress { get; set; }
        public string? MyRelationship { get; set; }
        public string? MyPhoneNumber { get; set; }
        [Required(ErrorMessage = "Không được để trống căn CCCD")]
        public string CitizenIDNumber { get; set; }
        public string? DataType { get; set; }
        public DateTime? DateEnd { get; set; }
        [Required(ErrorMessage = "Không được để trống ngày sinh")]
        public DateTime? DateOfBirth { get; set; }
        public string? CodeWards { get; set; }
        public string? NameWards { get; set; }
        public decimal? YearsOfWork { get; set; }
        public string? CreateBy { get; set; }
        public string? CreditProductCode { get; set; }
        public string? CreditProductName { get; set; }
    }
}
