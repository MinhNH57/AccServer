using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;

namespace Supply.Infrastructure;

public class SupplyDbContext : MultiTenantBaseContext, IUnitOfWork
{
    public DbSet<SULedger> SULedgers { get; set; }
    public DbSet<SUIncrement> SUIncrements { get; set; }
    public DbSet<SUIncrementDetailAllocation> SUIncrementDetailAllocations { get; set; }
    public DbSet<SUIncrementDetailSource> SUIncrementDetailSources { get; set; }
    public DbSet<SUIncrementDetail> SUIncrementDetails { get; set; }
    public DbSet<SUIncrementDetailDepartment> SUIncrementDetailDepartments { get; set; }
    public DbSet<SUDecrement> SUDecrements { get; set; }
    public DbSet<SUDecrementDetail> SUDecrementDetails { get; set; }
    public DbSet<SUTransfer> SUTransfers { get; set; }
    public DbSet<SUTransferDetail> SUTransferDetails { get; set; }
    public DbSet<SUAllocation> SUAllocations { get; set; }
    public DbSet<SUAllocationDetailExpense> SUAllocationDetailExpenses { get; set; }
    public DbSet<SUAllocationDetailTable> SUAllocationDetailTables { get; set; }
    public DbSet<SUAllocationDetailPost> SUAllocationDetailPosts { get; set; }
    public DbSet<SUAdjustment> SUAdjustments { get; set; }
    public DbSet<SUAdjustmentDetail> SUAdjustmentDetails { get; set; }
    public DbSet<SUAdjustmentDetailVoucher> SUAdjustmentDetailVouchers { get; set; }
    public DbSet<SUAudit> SUAudits { get; set; }
    public DbSet<SUAuditDetail> SUAuditDetails { get; set; }

    private readonly IMediator _mediator;

    private IDbContextTransaction _currentTransaction;

    public SupplyDbContext(IMultiTenantContextAccessor multiTenantContextAccessor)
        : base(multiTenantContextAccessor) { }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public SupplyDbContext(IMultiTenantContextAccessor multiTenantContextAccessor,
        DbContextOptions<SupplyDbContext> options, IMediator mediator)
        : base(multiTenantContextAccessor, options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("SupplyDbContext::ctor ->" + GetHashCode());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupplyDbContext).Assembly);
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
