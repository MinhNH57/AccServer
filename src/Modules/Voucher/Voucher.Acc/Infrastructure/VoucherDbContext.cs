using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Features.Commands.SmartData.CreateVoucher.Models;
using Voucher.Acc.Model;
using Voucher.Acc.Model.Contracts;
using Voucher.Acc.Model.DebtOffSet;
using Voucher.Acc.Model.ProductPlant;

namespace Voucher.Acc.Infrastructure;

/// <remarks>
///     Add migrations using the following command inside the 'Voucher.API' project directory:
///     dotnet ef migrations add --context VoucherDbContext [migration-name]
/// </remarks>
public class VoucherDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;

    public VoucherDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
        _currentUser = currentUser;

    }

    public VoucherDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<VoucherDbContext> options, ICurrentUser currentUser) : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }

    public DbSet<SmartData> SmartDatas => Set<SmartData>();

    public DbSet<SmartContentsData> SmartContentsDatas => Set<SmartContentsData>();

    //public DbSet<SmartFileAttach> SmartFileAttaches => Set<SmartFileAttach>();
    public DbSet<SmartVatTaxList> SmartVatTaxList => Set<SmartVatTaxList>();
    public DbSet<SalesSmartData> SalesSmartData => Set<SalesSmartData>();
    public DbSet<SalesSmartContentsData> SalesSmartContentsData => Set<SalesSmartContentsData>();
    public DbSet<SalesSmartDataTemp> SalesSmartDataTemp => Set<SalesSmartDataTemp>();
    public DbSet<SalesSmartContentsDataTemp> SalesSmartContentsDataTemp => Set<SalesSmartContentsDataTemp>();
    public DbSet<SmartConclusionQualityControl> SmartConclusionQualityControl => Set<SmartConclusionQualityControl>();
    public DbSet<SmartContentsQuanlityControl> SmartContentsQuanlityControl => Set<SmartContentsQuanlityControl>();
    public DbSet<SmartContentsSupplierEvaluation> SmartContentsSupplierEvaluation => Set<SmartContentsSupplierEvaluation>();
    public DbSet<SmartLogsOfUsingVouchers> SmartLogsOfUsingVouchers => Set<SmartLogsOfUsingVouchers>();

    public DbSet<FundSmartMoneyPaid> FundSmartMoneyPaid => Set<FundSmartMoneyPaid>();
    public DbSet<CreditContract> CreditContract => Set<CreditContract>();
    public DbSet<CreditContractContents> CreditContractContents => Set<CreditContractContents>();
    public DbSet<SmartContentsDebtRepaymentPlan> SmartContentsDebtRepaymentPlans => Set<SmartContentsDebtRepaymentPlan>();
    public DbSet<SmartDataManufacture> SmartDataManufactures => Set<SmartDataManufacture>();
    public DbSet<SmartDataManufactureContents> SmartDataManufactureContents => Set<SmartDataManufactureContents>();
    public DbSet<SmartDataBillOfMaterials> SmartDataBillOfMaterials => Set<SmartDataBillOfMaterials>();
    public DbSet<SmartDataPaletProductions> SmartDataPaletProductions => Set<SmartDataPaletProductions>();
    public DbSet<SmartDataProductionPlan> SmartDataProductionPlans => Set<SmartDataProductionPlan>();
    public DbSet<SmartProductionPlan> SmartProductionPlans => Set<SmartProductionPlan>();
    public DbSet<SmartProductAttributeByOrder> SmartProductAttributeByOrders => Set<SmartProductAttributeByOrder>();
    public DbSet<RequiPaymentData> RequiPaymentData => Set<RequiPaymentData>();
    public DbSet<RequiPaymentDataContents> RequiPaymentDataContents => Set<RequiPaymentDataContents>();
    public DbSet<HeadInvoiceInputs> HeadInvoiceInputs => Set<HeadInvoiceInputs>();
    public DbSet<TradingSmartContentsData> TradingSmartContentsData => Set<TradingSmartContentsData>();
    public DbSet<PaymentPlanContents> PaymentPlanContents => Set<PaymentPlanContents>();
    public DbSet<SmartPaymentVendor> SmartPaymentVendors => Set<SmartPaymentVendor>();
    public DbSet<SmartDebtOffSet> SmartDebtOffSets => Set<SmartDebtOffSet>();
    public DbSet<TravelExpenses> TravelExpenses  => Set<TravelExpenses>();
    public DbSet<HQDTOKHAIMD> HQDTOKHAIMD  => Set<HQDTOKHAIMD>();

    public DbSet<SmartDebtOffSetContents> SmartDebtOffSetContents => Set<SmartDebtOffSetContents>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<HeadInvoiceInputsView>().HasNoKey().ToView("HeadInvoiceInputsView");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoucherDbContext).Assembly);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var item in base.ChangeTracker.Entries<IBaseEntity>()
                     .Where(c => c.State is EntityState.Added or EntityState.Modified))
        {
            if (item.State == EntityState.Added)
            {
                int updateCount = 0;
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    if (updateCount == 3) break;
                    if (propertyInfo.Name == "CreatedBy" || propertyInfo.Name == "CreateBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        updateCount++;
                    }
                    else if (propertyInfo.Name is "CreatedDate" or "CreateDate")
                    {
                        propertyInfo.SetValue(item.Entity, DateTime.Now);
                        updateCount++;
                    }
                }
            }
            else if (item.State == EntityState.Modified)
            {
                int updateCount = 0;
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    if (updateCount == 3) break;
                    if (propertyInfo.Name is "ModifiedBy" or "ModifyBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        updateCount++;
                    }
                    else if (propertyInfo.Name is "ModifiedDate" or "Modified" or "ModifyDate")
                    {
                        propertyInfo.SetValue(item.Entity, DateTime.Now);
                        updateCount++;
                    }
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
