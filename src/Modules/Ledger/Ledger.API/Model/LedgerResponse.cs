using System;

namespace Ledger.API.Model;

public class LedgerResponse
{
    public Guid RefId { get; set; }
    public int RefType { get; set; }
    public string TableName { get; set; }
    public int EditVersion { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string MessageType { get; set; }
    public int DisplayOnBook { get; set; }
    public bool IsPostToManagementBook { get; set; }
    public bool AllowOverOutwardStock { get; set; }
    public bool IsList { get; set; }
    public bool IsPassAllWarning { get; set; }
    public bool IsSyncParam { get; set; }
    public int TypeStopPostUnPost { get; set; }
    public DateTime RefDate { get; set; }
    public DateTime PostedDate { get; set; }
    public string RefNo { get; set; }
    public bool IsNotValidateVersion { get; set; }
    public bool IsPostAfterSave { get; set; }
    public bool IsCalPriceAfterPost { get; set; }
    public bool IsSyncInventoryBalance { get; set; }
    public int CrossType { get; set; }
    public bool ReceiptType { get; set; }
    public bool OldIsPostedFinance { get; set; }
    public bool IsCallbackPublishInvoice { get; set; }
}
