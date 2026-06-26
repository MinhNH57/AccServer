using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartAccCloud.Application.Caching;
using SmartAccCloud.Application.Interfaces.ImpExp;
using SmartAccCloud.Application.Services.Catalogs.BankAccountForObject;
using SmartAccCloud.Application.Services.Catalogs.BillOfMaterials;
using SmartAccCloud.Application.Services.Catalogs.CatalogAsset;
using SmartAccCloud.Application.Services.Catalogs.CatalogObject;
using SmartAccCloud.Application.Services.Catalogs.Dependents;
using SmartAccCloud.Application.Services.Catalogs.ProductForAsset;
using SmartAccCloud.Application.Services.Catalogs.ProductForContract;
using SmartAccCloud.Application.Services.Catalogs.QrCodeForProduct;
using SmartAccCloud.Application.Services.Extension;
using SmartAccCloud.Application.Services.FileWork;
using SmartAccCloud.Application.Services.ImpExp;
using SmartAccCloud.Application.Services.Salary.SalaryTimeSheet;
using SmartAccCloud.Application.Services.Salary.SalaryTimeSheetSummaryDetails;
using SmartAccCloud.Application.Services.SmartFund;
using SmartAccCloud.Application.Services.TonMyAnh;

namespace SmartAccCloud.Application;

public static class DependenceInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddServices(configuration);
        return services;
    }

    private static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Add service here
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IExtensionServices, ExtensionServices>();
        services.AddScoped<ICatalogObjectServices, CatalogObjectServices>();
        services.AddScoped<ISmartVatListService, SmartVatListService>();
        services.AddScoped<IFileAttachServices, FileAttachServices>();
        services.AddScoped<IProductForContractServices, ProductForContractServices>();
        services.AddScoped<ICatalogAssetServices, CatalogAssetServices>();
        services.AddScoped<IProductForAssetServices, ProductForAssetServices>();
        services.AddScoped<IBillOfMaterialsServices, BillOfMaterialsServices>();
        services.AddScoped<IQrCodeForProductServices, QrCodeForProductServices>();
        services.AddScoped<IBankAccountForObjectServices, BankAccountForObjectServices>();
        services.AddScoped<IDependentsServices, DependentsServices>();
        services.AddScoped<ISalaryTimeSheetServices, SalaryTimeSheetServices>();
        services.AddScoped<ISalaryTimeSheetSummaryDetailsServices, SalaryTimeSheetSummaryDetailsServices>();
        services.AddScoped<ITonMoblieService, TonMoblieService>();
        services.AddScoped<ISmartFundServices, SmartFundServices>();
        
        services.AddScoped<IImpExpService, ImpExpService>();
        //Add http client
        services.AddHttpClient();

    }
}