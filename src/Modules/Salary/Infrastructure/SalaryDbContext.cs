using Salary.Model;
using System.Collections.Generic;
using System.Reflection.Emit;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Salary.Model.Contracts;

namespace Salary.Infrastructure
{
    public class SalaryDbContext : MultiTenantBaseContext
    {
        private readonly ICurrentUser _currentUser;
       
        public SalaryDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
        {
            _currentUser = currentUser;

        }

        public SalaryDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<SalaryDbContext> options, ICurrentUser currentUser) : base(multiTenantContextAccessor, options)
        {
            _currentUser = currentUser;
        }

        public DbSet<CatalogSalaryInsuranceRegulations> CatalogSalaryInsuranceRegulations => Set<CatalogSalaryInsuranceRegulations>();
        public DbSet<CatalogTimekeepingSymbols> CatalogTimekeepingSymbols => Set<CatalogTimekeepingSymbols>();
        public DbSet<SalarySummaryOfTimekeeping> SalarySummaryOfTimekeeping => Set<SalarySummaryOfTimekeeping>();
        public DbSet<SalaryTimeSheet> SalaryTimeSheet => Set<SalaryTimeSheet>(); 
        public DbSet<SalaryTimeSheetSummary> SalaryTimeSheetSummary => Set<SalaryTimeSheetSummary>();
        public DbSet<SalaryTimeSheetSummaryDetails> SalaryTimeSheetSummaryDetails => Set<SalaryTimeSheetSummaryDetails>();
        public DbSet<SalaryTimeSheetDetail> SalaryTimeSheetDetail => Set<SalaryTimeSheetDetail>();
        public DbSet<SalaryTimeSheetHead> SalaryTimeSheetHead => Set<SalaryTimeSheetHead>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalaryDbContext).Assembly);
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
                        if (propertyInfo.Name == "ModifiedBy" || propertyInfo.Name == "ModifyBy")
                        {
                            propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                            updateCount++;
                        }
                        else if (propertyInfo.Name == "ModifiedDate")
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
}
