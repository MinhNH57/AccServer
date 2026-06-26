using System.ComponentModel.DataAnnotations;

namespace Catalog.Base.Entities
{
    public class CatalogAccountSymbol
    {
        [Key]
        [Required(ErrorMessage = "Số hiệu tài khoản không được để trống")]
        public string AccountSymbol { get; set; } = null!;
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string AccountName { get; set; } = null!;
        public int AccountLevel { get; set; }
        [Required(ErrorMessage = "Loại tài khoản không được để trống")]
        public string AccountType { get; set; } = null!;
        public string? AccountParent { get; set; }
        public bool IsParent { get; set; }
        public string? Notes { get; set; }
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string? CodeReport { get; set; }
        public int? CodeUnit { get; set; }
        //public DateTime Created { get; set; } = DateTime.Now;
        //public string? CreatedBy { get; set; }
        //public DateTime? Modified { get; set; }
        //public string? ModifiedBy { get; set; }
        public string? ObjectCodeAllotment { get; set; }
        public bool IsTax { get; set; }
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
        public bool IsActive { get; set; }
        public bool Debit { get; set; }
        public bool Credit { get; set; }
        public bool NoBalance { get; set; }
        public bool IsObligatory { get; set; }
        public bool IsContract { get; set; }
        public bool IsConstruction { get; set; }
        public bool IsProject { get; set; }
        public bool IsRevenueExpense { get; set; }
        public bool IsForeignCurrency { get; set; }
        public bool IsTools { get; set; }
        public bool IsPrice { get; set; }
        public string? IsMaterial { get; set; } = "0";
        public bool IsWarehose { get; set; }
        public string? IsObject { get; set; } = "0";
        public bool IsInvoice { get; set; }
        public bool IsRoom { get; set; }
        public bool IsProductionActivities { get; set; }
        public bool IsFundingSource { get; set; }
        public bool IsAccountAllotMent { get; set; }
        public bool IsHermaphrodite { get; set; }
        public bool IsCodeReport { get; set; }
        public bool ObjectTHCP { get; set; }
        public bool IsObjectTHCP { get; set; }
        public bool IsNoConsolidation { get; set; }
        public bool NoConsolidation { get; set; }
        public bool IsNotMerge { get; set; }
        public int CreditDebitType { get; set; }
        public bool RevExp { get; set; }
        public bool IsRevExp { get; set; }
        public bool CostAggregation { get; set; }
        public bool IsCostAggregation { get; set; }
    }
}
