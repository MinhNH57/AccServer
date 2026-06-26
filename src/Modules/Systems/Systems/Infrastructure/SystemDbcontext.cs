using Finbuckle.MultiTenant.Abstractions;
using Systems.Infrastructure.Entities;
using Systems.Infrastructure.Entities.HRM;
using Systems.Infrastructure.Entities.SmartFund;
using Lock = Systems.Infrastructure.Entities.Lock;

namespace Systems.Infrastructure;

public class SystemDbcontext : MultiTenantBaseContext
{
    public SystemDbcontext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public SystemDbcontext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<SystemDbcontext> options) : base(multiTenantContextAccessor, options)
    {
    }

    public DbSet<SmartOption> SmartOption => Set<SmartOption>();
    public DbSet<Menu> WSmartMenu => Set<Menu>();
    public DbSet<CatalogFunction> CatalogFuntion => Set<CatalogFunction>();
    public DbSet<RuleUser> RuleUser => Set<RuleUser>();
    public DbSet<RuleUnit> RuleUnit => Set<RuleUnit>();
    public DbSet<RuleAccount> RuleAccount => Set<RuleAccount>();
    public DbSet<Users> Users => Set<Users>();
    public DbSet<CatalogVoucherNumber> CatalogVoucherNumber => Set<CatalogVoucherNumber>();
    public DbSet<RuleBaseUnion> RuleBaseUnion => Set<RuleBaseUnion>();
    public DbSet<UnitInfo> UnitInfo => Set<UnitInfo>();
    public DbSet<ComponentSetting> ComponentSettings => Set<ComponentSetting>();

    public DbSet<ComponentProperty> ComponentProperties => Set<ComponentProperty>();

    public DbSet<ContextMenuProperty> ContextMenuProperties => Set<ContextMenuProperty>();
    public DbSet<Lock> Locks => Set<Lock>();
    public DbSet<SmartMapColumnExcel> SmartMapColumnExcel => Set<SmartMapColumnExcel>();
    public DbSet<CatalogRecipientOfMessage> CatalogRecipientOfMessage => Set<CatalogRecipientOfMessage>();
    public DbSet<ViewRecipientOfMessageByOwner> ViewRecipientOfMessageByOwners => Set<ViewRecipientOfMessageByOwner>();

    #region HRM
    public DbSet<HRM_UsageConfiguration> HRM_UsageConfiguration => Set<HRM_UsageConfiguration>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemDbcontext).Assembly);
    }
}