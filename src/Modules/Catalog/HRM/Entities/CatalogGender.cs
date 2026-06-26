namespace Catalog.HRM.Entities;

public class CatalogGender
{
    public Guid? Id { get; set; }                     // PK
/*        public int? IdAsc { get; set; }  */                 // STT
    public string? GenderCode { get; set; }          // Mã giới tính
    public string? GenderName { get; set; }          // Tên giới tính
    public int? CodeUnit { get; set; }               // Mã đơn vị
    public string? Notes { get; set; }               // Ghi chú
    public bool? IsActive { get; set; } = true;
}