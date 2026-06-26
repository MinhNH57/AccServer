using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Services.CreateRuleFucnAndReport;

public record CreateRuleFucnAndReportRequest(string Param, string CodeUser, int CodeUnit);

public interface ICreateRuleFucnAndReportService
{
    Task<bool> CreateRuleFucnAndReportAsync(CreateRuleFucnAndReportRequest request);
}

public class CreateRuleFucnAndReportService(IMultiTenantContextAccessor tenantContextAccessor, IDataServices dataServices) : ICreateRuleFucnAndReportService
{
    TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
    public async Task<bool> CreateRuleFucnAndReportAsync(CreateRuleFucnAndReportRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        var obj = new
        {
            request.Param,
            request.CodeUser,
            request.CodeUnit
        };
        
        return await dataServices.ExcuteNonQueryAsync("CreateRuleFucnAndReport", _tenant.ConnectionString(), obj);
    }
}