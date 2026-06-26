using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartAccCloud.Domain.Validation;

namespace SmartAccCloud.Domain.Entity.Catalogs
{
    public class CatalogAccountSymbol
    {
        [Key]
        [Unique(nameof(CatalogAccountSymbol), nameof(AccountSymbol), ErrorMessage = "Giá trị này đã tồn tại")]
        
        [Required(ErrorMessage = "Số hiệu tài khoản không được để trống")]
        public string AccountSymbol { get; set; }
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string AccountName { get; set; }
        public int? AccountLevel { get; set; }
        [Required(ErrorMessage = "Loại tài khoản không được để trống")]
        public string AccountType { get; set; }
        public bool Obligatory { get; set; }
        public bool Contract { get; set; }
        public bool Construction { get; set; }
        public bool Project { get; set; }
        public bool RevenueExpense { get; set; }
        public bool ForeignCurrency { get; set; }
        public bool Tools { get; set; }
        public bool Price { get; set; }
        public bool Materials { get; set; }
        public bool Warehose { get; set; }
        public bool Object { get; set; }
        public bool Invoice { get; set; }
        public bool Room { get; set; }
        public bool ProductionActivities { get; set; }
        public bool FundingSource { get; set; }
        public bool AccountAllotment { get; set; }
        public bool Hermaphrodite { get; set; }
        public string? CodeReport { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int IdAsc { get; set; }
        public int? CodeUnit { get; set; } = 100;
        public bool IsActive { get; set; }
        public bool? Debit { get; set; }
        public bool? Credit { get; set; }
        public bool? NoBalance { get; set; }
        public bool IsTax { get; set; }
        public bool? IsObligatory { get; set; }
        public bool? IsContract { get; set; }
        public bool? IsConstruction { get; set; }
        public bool? IsProject { get; set; }
        public bool? IsRevenueExpense { get; set; }
        public bool? IsForeignCurrency { get; set; }
        public bool? IsTools { get; set; }
        public bool? IsPrice { get; set; }
        public bool? IsMaterial { get; set; }
        public bool? IsWarehose { get; set; }
        public bool? IsObject { get; set; }
        public bool? IsInvoice { get; set; }
        public bool? IsRoom { get; set; }
        public bool? IsProductionActivities { get; set; }
        public bool? IsFundingSource { get; set; }
        public bool? IsAccountAllotMent { get; set; }
        public bool? IsHermaphrodite { get; set; }
        public bool? IsCodeReport { get; set; }
        public bool? ObjectTHCP { get; set; }
        public bool? IsObjectTHCP { get; set; }
        public bool NoConsolidation { get; set; }
        public bool IsNoConsolidation { get; set; }
        public string? AccountParent { get; set; }
    }
}
