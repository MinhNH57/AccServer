using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;

namespace FixedAsset.Infrastructure;

public class FixedAssetDbContext : MultiTenantBaseContext, IUnitOfWork
{
    public DbSet<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset> FixedAssets { get; set; }
    public DbSet<FixedAssetDetailAllocation> FixedAssetDetailAllocations { get; set; }
    public DbSet<FixedAssetDetailSource> FixedAssetDetailSources { get; set; }
    public DbSet<FixedAssetDetail> FixedAssetDetails { get; set; }
    public DbSet<FixedAssetDetailAccessory> FixedAssetDetailAccessories { get; set; }
    public DbSet<FADecrement> FADecrements { get; set; }
    public DbSet<FADecrementDetail> FADecrementDetails { get; set; }
    public DbSet<FADecrementDetailPost> FADecrementDetailPosts { get; set; }
    public DbSet<FATransfer> FATransfers { get; set; }
    public DbSet<FATransferDetail> FATransferDetails { get; set; }
    public DbSet<FADepreciation> FADepreciations { get; set; }
    public DbSet<FADepreciationDetail> FADepreciationDetails { get; set; }
    public DbSet<FADepreciationDetailAllocation> FADepreciationDetailAllocations { get; set; }
    public DbSet<FADepreciationDetailPost> FADepreciationDetailPosts { get; set; }
    public DbSet<FAAdjustment> FAAdjustments { get; set; }
    public DbSet<FAAdjustmentDetail> FAAdjustmentDetails { get; set; }
    public DbSet<FAAdjustmentDetailPost> FAAdjustmentDetailPosts { get; set; }
    public DbSet<FAAudit> FAAudits { get; set; }
    public DbSet<FAAuditDetail> FAAuditDetails { get; set; }
    public DbSet<FAChangeFinancialLeasingToOwner> FAChangeFinancialLeasingToOwners { get; set; }
    public DbSet<FAChangeFinancialLeasingToOwnerDetail> FAChangeFinancialLeasingToOwnerDetails { get; set; }

    private readonly IMediator _mediator;

    private IDbContextTransaction _currentTransaction;

    public FixedAssetDbContext(IMultiTenantContextAccessor multiTenantContextAccessor)
        : base(multiTenantContextAccessor) { }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public FixedAssetDbContext(IMultiTenantContextAccessor multiTenantContextAccessor,
        DbContextOptions<FixedAssetDbContext> options, IMediator mediator)
        : base(multiTenantContextAccessor, options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("FixedAssetDbContext::ctor ->" + GetHashCode());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FixedAssetDbContext).Assembly);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (HasActiveTransaction)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}

#nullable enable
