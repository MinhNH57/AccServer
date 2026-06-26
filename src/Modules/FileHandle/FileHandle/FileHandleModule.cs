using System.Reflection;
using BuildingBlocks.Dapper;
using BuildingBlocks.Messaging.MassTransit;
using BuildingBlocks.Web;
using FileHandle.Data;
using FileHandle.Services;
using FileHandle.Services.HRM;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FileHandle;

public static class FileHandleModule
{
    public static IServiceCollection AddFileHandleModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileHandleDbContext>();
        services.AddDbContext<HRMFileHandleDbContext>();
        services.AddScoped<SmartDataServices>();
        services.AddScoped<IFileAttachServices, FileAttachServices>();
        services.AddScoped<IFileHandleHRMService, FileHandleHrmService>();
        services.AddScoped<IHandleExcelFileService, HandleExcelFileService>();
        services.AddScoped<IExcelSmartDataServices, ExcelSmartDataServices>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddMesageBroker(configuration, Assembly.GetExecutingAssembly());
        return services;
    }
}