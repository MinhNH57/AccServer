using GenericServices;

namespace SmartAccCloud.Application.Models.Catalogs.CatalogVoucherNumber;

public class CatalogVoucherNbVm : ILinkToEntity<Domain.Entity.Catalogs.CatalogVoucherNumber>
{
    public string? DataType { get; set; }
    public string? SignVoucher { get; set; }
    public int? VoucherLength { get; set; }
    public string? VoucherName { get; set; }
    public string? DebitSide { get; set; }
    public string? CreditSide { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public bool ByDate { get; set; }
    public bool VatTab { get; set; }
    public Guid Id { get; set; }
    public int? CodeUnit { get; set; }
    public bool? DataImpExp { get; set; }
    public bool? DataInvoice { get; set; }
    public bool? DataVoucher { get; set; }
    public bool? DataConsign { get; set; }
    public bool DataOption { get; set; }
    public bool TaxSeparation { get; set; }
    public bool SaveDataLogs { get; set; }
    public bool ShipmentNumber { get; set; }
    public int? NumberDayEditVouchers { get; set; }
    public bool Censorship { get; set; }
    public bool LocationWarehouse { get; set; }
    public bool DebitNumber { get; set; }
}