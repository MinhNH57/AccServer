using System.Data;
using BuildingBlocks.Pagination.Version1;
using BuildingBlocks.Response;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Features.Queries.GetVoucherStore;
using Voucher.Acc.Model;
using Voucher.Acc.Model.DebtOffSet;

namespace Voucher.Acc.Apis;

public static class VoucherApi
{
    public static RouteGroupBuilder MapVoucherApiV1(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/").HasApiVersion(1.0).RequireAuthorization();

        // Routes for querying object.

        api.MapGet("{id:guid}", FindOne)
            .WithName("Voucher")
            .WithSummary("Nhận một phiếu.")
            .WithDescription("Nhận một phiếu theo Id.")
            .WithTags("Vouchers");

        // Routes for modifying object.

        api.MapPost("", Create)
            .WithName("CreateVoucher")
            .WithSummary("Tạo một phiếu.")
            .WithDescription("Tạo một phiếu mới.")
            .WithTags("Vouchers");

        api.MapPut("{id:guid}", Update)
            .WithName("UpdateVoucher")
            .WithSummary("Tạo hoặc thay thế một phiếu.")
            .WithDescription("Tạo hoặc thay thế một phiếu.")
            .WithTags("Vouchers");

        api.MapDelete("{id:guid}", Delete)
            .WithName("DeleteVoucher")
            .WithSummary("Xóa phiếu.")
            .WithDescription("Xóa một phiếu chỉ định.")
            .WithTags("Vouchers");

        api.MapGet("voucher-sales/{id:guid}", FindOneSales)
            .WithName("SalesVoucher")
            .WithSummary("Get phiếu theo ID.")
            .WithDescription("Get phiếu theo Id và lấy ra nội dung.")
            .WithTags("Vouchers");
        api.MapGet("debt-off-set/{id:guid}", FindOneDebtOffSet)
            .WithName("DebtOffSetVoucher")
            .WithSummary("Get phiếu theo ID.")
            .WithDescription("Get phiếu theo Id và lấy ra nội dung.")
            .WithTags("Vouchers");
        return api;
    }


    private static async Task<Ok<ApiResponse<SmartData>>> FindOne(
    [AsParameters] VoucherServices services,
    //[FromHeader(Name = HeaderContains.RequestIdHeader)][Required] Guid requestId,
    [FromRoute] Guid id)
    {
        //var smartData = await services.Context.SmartDatas.FirstOrDefaultAsync(c => c.Id == id);
        var smartData = await services.Context.SmartDatas
            .Include(x => x.SmartContentsDatas.OrderBy(y => y.CreateDate))
            .Include(x => x.SmartFileAttaches)
            .Include(x => x.SmartVatTaxLists)
            .Include(x => x.SmartPaymentVendors)
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<SmartData>.Ok(smartData);

        return TypedResults.Ok(response);
    }

    private static async Task<Results<Created<ApiResponse<SmartData>>, BadRequest<ApiResponse<string>>>> Create(
        [AsParameters] VoucherServices services,
        SmartData itemToCreate)
    {
        try
        {
            if (itemToCreate != null)
            {
                throw new Exception();
            }

            itemToCreate.Id = Guid.NewGuid();
            itemToCreate.CreatedDate = DateTime.Now;

            services.Context.SmartDatas.Add(itemToCreate);

            await services.Context.SaveChangesAsync();

            var response = await services.Context.SmartDatas
                .Include(x => x.SmartContentsDatas.OrderBy(y => y.CreateDate))
                .Include(x => x.SmartFileAttaches)
                .FirstOrDefaultAsync(x => x.Id == itemToCreate.Id);

            return TypedResults.Created($"/api/v1/smartData/{itemToCreate.Id}", ApiResponseFactory<SmartData>.Created(response));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private static async Task<Results<Ok<ApiResponse<SmartData>>, BadRequest<ApiResponse<string>>>> Update(
        [AsParameters] VoucherServices services,
        Guid id,
        SmartData itemToUpdate)
    {
        try
        {
            var smartData = await services.Context.SmartDatas
                .Include(x => x.SmartContentsDatas.OrderBy(y => y.CreateDate))
                .Include(x => x.SmartFileAttaches)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (smartData == null)
            {
                throw new Exception($"Không tìm thấy cấu hình với Id : {id}.");
            }
            else
            {
                #region CMt

                //smartData.DataType = itemToUpdate.DataType;
                //smartData.DataName = itemToUpdate.DataName;
                //smartData.CostAllotment = itemToUpdate.CostAllotment;
                //smartData.VoucherDate = itemToUpdate.VoucherDate;
                //smartData.RecordDate = itemToUpdate.RecordDate;
                //smartData.NumberOfVouchers = itemToUpdate.NumberOfVouchers;
                //smartData.InvoiceNumber = itemToUpdate.InvoiceNumber;
                //smartData.PersonCode = itemToUpdate.PersonCode;
                //smartData.PersonName = itemToUpdate.PersonName;
                //smartData.PersonAddress = itemToUpdate.PersonAddress;
                //smartData.PersonTaxCode = itemToUpdate.PersonTaxCode;
                //smartData.ObjectCode = itemToUpdate.ObjectCode;
                //smartData.ObjectName = itemToUpdate.ObjectName;
                //smartData.ObjectAddress = itemToUpdate.ObjectAddress;
                //smartData.ObjectTaxCode = itemToUpdate.ObjectTaxCode;
                //smartData.GroupAreaCode = itemToUpdate.GroupAreaCode;
                //smartData.GroupAreaName = itemToUpdate.GroupAreaName;
                //smartData.GroupCode = itemToUpdate.GroupCode;
                //smartData.GroupName = itemToUpdate.GroupName;
                //smartData.WarehoseCode = itemToUpdate.WarehoseCode;
                //smartData.WarehoseName = itemToUpdate.WarehoseName;
                //smartData.WarehoseCodeReceive = itemToUpdate.WarehoseCodeReceive;
                //smartData.WarehoseNameReceive = itemToUpdate.WarehoseNameReceive;
                //smartData.ReasonCode = itemToUpdate.ReasonCode;
                //smartData.ReasonName = itemToUpdate.ReasonName;
                //smartData.ShippingMethodsCode = itemToUpdate.ShippingMethodsCode;
                //smartData.ShippingMethodsName = itemToUpdate.ShippingMethodsName;
                //smartData.MethodOfPaymentsCode = itemToUpdate.MethodOfPaymentsCode;
                //smartData.MethodOfPaymentsName = itemToUpdate.MethodOfPaymentsName;
                //smartData.Description = itemToUpdate.Description;
                //smartData.Notes = itemToUpdate.Notes;
                //smartData.Pricing = itemToUpdate.Pricing;
                //smartData.EInvoice = itemToUpdate.EInvoice;
                //smartData.InvocePublished = itemToUpdate.InvocePublished;
                //smartData.KeyInvoce = itemToUpdate.KeyInvoce;
                //smartData.ENumberInvoice = itemToUpdate.ENumberInvoice;
                //smartData.ENumberInvoiceDraft = itemToUpdate.ENumberInvoiceDraft;
                //smartData.InvoiceResult = itemToUpdate.InvoiceResult;
                //smartData.IdDataInherit = itemToUpdate.IdDataInherit;
                //smartData.NotEnvironment = itemToUpdate.NotEnvironment;
                //smartData.VehiclesName = itemToUpdate.VehiclesName;
                //smartData.VoucherStatus = itemToUpdate.VoucherStatus;
                //smartData.LicensePlates = itemToUpdate.LicensePlates;
                //smartData.InvoiceSymbol = itemToUpdate.InvoiceSymbol;
                //smartData.InvoiceTemplate = itemToUpdate.InvoiceTemplate;
                //smartData.SignTransfer = itemToUpdate.SignTransfer;
                //smartData.Register = itemToUpdate.Register;
                //smartData.CodeUnit = itemToUpdate.CodeUnit;
                //smartData.IsActive = itemToUpdate.IsActive;
                //smartData.CreateDate = itemToUpdate.CreateDate;
                //smartData.CreateBy = itemToUpdate.CreateBy;
                //smartData.ModifyDate = itemToUpdate.ModifyDate;
                //smartData.ModifyBy = itemToUpdate.ModifyBy;
                //smartData.Selectted = itemToUpdate.Selectted;
                //smartData.InvoiceCancel = itemToUpdate.InvoiceCancel;
                //smartData.ObjectEmail = itemToUpdate.ObjectEmail;
                //smartData.LOAIPHIEU = itemToUpdate.LOAIPHIEU;
                //smartData.IdDataHead = itemToUpdate.IdDataHead;
                //smartData.ComfirmVoucher = itemToUpdate.ComfirmVoucher;
                //smartData.VoucherNoInherit = itemToUpdate.VoucherNoInherit;
                //smartData.Delivered = itemToUpdate.Delivered;
                //smartData.ContractNo = itemToUpdate.ContractNo;
                //smartData.Vat = itemToUpdate.Vat;
                //smartData.SaveTemp = itemToUpdate.SaveTemp;
                //smartData.IdVoucherSource = itemToUpdate.IdVoucherSource;
                //smartData.SignAdjust = itemToUpdate.SignAdjust;
                //smartData.MemberRate = itemToUpdate.MemberRate;
                //smartData.ForeignCurrencyType = itemToUpdate.ForeignCurrencyType;
                //smartData.ExchangeRate = itemToUpdate.ExchangeRate;
                //smartData.PurchasePurposeCode = itemToUpdate.PurchasePurposeCode;
                //smartData.PurchasePurposeName = itemToUpdate.PurchasePurposeName;
                //smartData.Created = itemToUpdate.Created;
                //smartData.CreatedBy = itemToUpdate.CreatedBy;
                //smartData.Modified = itemToUpdate.Modified;
                //smartData.ModifiedBy = itemToUpdate.ModifiedBy;
                //smartData.MaturityDate = itemToUpdate.MaturityDate;
                //smartData.ContactPerson = itemToUpdate.ContactPerson;
                //smartData.IsCPMuaHang = itemToUpdate.IsCPMuaHang;
                //smartData.PaymentVoucherNumber = itemToUpdate.PaymentVoucherNumber;
                //smartData.VatRate = itemToUpdate.VatRate;
                //smartData.DiscountTypeCode = itemToUpdate.DiscountTypeCode;
                //smartData.DiscountTypeName = itemToUpdate.DiscountTypeName;
                //smartData.DiscountValue = itemToUpdate.DiscountValue;
                //smartData.Status = itemToUpdate.Status;
                //smartData.SmartContentsDatas = itemToUpdate.SmartContentsDatas;
                //smartData.SmartFileAttaches = itemToUpdate.SmartFileAttaches;

                #endregion

                services.Mapper.Map(itemToUpdate, smartData);

                smartData.SmartContentsDatas = smartData.SmartContentsDatas
                    .Where(x => itemToUpdate.SmartContentsDatas
                        .Select(y => y.IdSource).ToList().Contains(x.IdSource)).ToList();

                foreach (var smartContentsData in itemToUpdate.SmartContentsDatas)
                {
                    var updateChildItem = smartData.SmartContentsDatas.Find(x => x.IdSource == smartContentsData.IdSource);
                    if (updateChildItem != null)
                    {

                        #region Cmt

                        //updateChildItem.IdContents = smartContentsData.IdContents;
                        //updateChildItem.DataType = smartContentsData.DataType;
                        //updateChildItem.DebitSide = smartContentsData.DebitSide;
                        //updateChildItem.CreditSide = smartContentsData.CreditSide;
                        //updateChildItem.AccountSymbol = smartContentsData.AccountSymbol;
                        //updateChildItem.CommodityCode = smartContentsData.CommodityCode;
                        //updateChildItem.CommodityName = smartContentsData.CommodityName;
                        //updateChildItem.WarehoseCode = smartContentsData.WarehoseCode;
                        //updateChildItem.WarehoseName = smartContentsData.WarehoseName;
                        //updateChildItem.WarehoseCodeReceive = smartContentsData.WarehoseCodeReceive;
                        //updateChildItem.WarehoseNameReceive = smartContentsData.WarehoseNameReceive;
                        //updateChildItem.ProductCode = smartContentsData.ProductCode;
                        //updateChildItem.ProductName = smartContentsData.ProductName;
                        //updateChildItem.UnitPcs = smartContentsData.UnitPcs;
                        //updateChildItem.UnitPackage = smartContentsData.UnitPackage;
                        //updateChildItem.ConversionFactor = smartContentsData.ConversionFactor;
                        //updateChildItem.PackageQuantity = smartContentsData.PackageQuantity;
                        //updateChildItem.QuantityOfInventory = smartContentsData.QuantityOfInventory;
                        //updateChildItem.Quantity = smartContentsData.Quantity;
                        //updateChildItem.Quantity15 = smartContentsData.Quantity15;
                        //updateChildItem.RetailPrice = smartContentsData.RetailPrice;
                        //updateChildItem.RetailPriceForeignCurrency = smartContentsData.RetailPriceForeignCurrency;
                        //updateChildItem.Price = smartContentsData.Price;
                        //updateChildItem.PriceForeignCurrency = smartContentsData.PriceForeignCurrency;
                        //updateChildItem.AmountOfMoney = smartContentsData.AmountOfMoney;
                        //updateChildItem.AmountOfMoneyForeignCurrency = smartContentsData.AmountOfMoneyForeignCurrency;
                        //updateChildItem.AmountOfMoneyUsd = smartContentsData.AmountOfMoneyUsd;
                        //updateChildItem.AmountVat = smartContentsData.AmountVat;
                        //updateChildItem.AmountVatForeignCurrency = smartContentsData.AmountVatForeignCurrency;
                        //updateChildItem.AmountWithoutVat = smartContentsData.AmountWithoutVat;
                        //updateChildItem.AmountWithoutVatForeignCurrency = smartContentsData.AmountWithoutVatForeignCurrency;
                        //updateChildItem.ForeignCurrencyType = smartContentsData.ForeignCurrencyType;
                        //updateChildItem.ExchangeRate = smartContentsData.ExchangeRate;
                        //updateChildItem.VatType = smartContentsData.VatType;
                        //updateChildItem.VatRate = smartContentsData.VatRate;
                        //updateChildItem.DiscountRate = smartContentsData.DiscountRate;
                        //updateChildItem.AmountDiscount = smartContentsData.AmountDiscount;
                        //updateChildItem.AmountDiscountForeignCurrency = smartContentsData.AmountDiscountForeignCurrency;
                        //updateChildItem.AmountAfterDiscount = smartContentsData.AmountAfterDiscount;
                        //updateChildItem.AmountAfterDiscountForeignCurrency = smartContentsData.AmountAfterDiscountForeignCurrency;
                        //updateChildItem.FeeEnvironRate = smartContentsData.FeeEnvironRate;
                        //updateChildItem.AmountFeeEnvironRate = smartContentsData.AmountFeeEnvironRate;
                        //updateChildItem.CostPrice = smartContentsData.CostPrice;
                        //updateChildItem.CostOfGoodsSold = smartContentsData.CostOfGoodsSold;
                        //updateChildItem.DebitObjectCode = smartContentsData.DebitObjectCode;
                        //updateChildItem.DebitObjectName = smartContentsData.DebitObjectName;
                        //updateChildItem.DebitObjectTax = smartContentsData.DebitObjectTax;
                        //updateChildItem.CreditObjectCode = smartContentsData.CreditObjectCode;
                        //updateChildItem.CreditObjectName = smartContentsData.CreditObjectName;
                        //updateChildItem.CreditObjectTax = smartContentsData.CreditObjectTax;
                        //updateChildItem.InvoiceNumberContents = smartContentsData.InvoiceNumberContents;
                        //updateChildItem.Description = smartContentsData.Description;
                        //updateChildItem.RevenueExpenseCode = smartContentsData.RevenueExpenseCode;
                        //updateChildItem.RevenueExpenseName = smartContentsData.RevenueExpenseName;
                        //updateChildItem.ContractCode = smartContentsData.ContractCode;
                        //updateChildItem.ContractName = smartContentsData.ContractName;
                        //updateChildItem.ConstructionCode = smartContentsData.ConstructionCode;
                        //updateChildItem.ConstructionName = smartContentsData.ConstructionName;
                        //updateChildItem.ProjectCode = smartContentsData.ProjectCode;
                        //updateChildItem.ProjectName = smartContentsData.ProjectName;
                        //updateChildItem.RoomCode = smartContentsData.RoomCode;
                        //updateChildItem.RoomName = smartContentsData.RoomName;
                        //updateChildItem.ProductionActivitieCode = smartContentsData.ProductionActivitieCode;
                        //updateChildItem.ProductionActivitieName = smartContentsData.ProductionActivitieName;
                        //updateChildItem.FundingSourceCode = smartContentsData.FundingSourceCode;
                        //updateChildItem.FundingSourceName = smartContentsData.FundingSourceName;
                        //updateChildItem.DebitSideOut = smartContentsData.DebitSideOut;
                        //updateChildItem.CreditSideOut = smartContentsData.CreditSideOut;
                        //updateChildItem.CoefficientVcf = smartContentsData.CoefficientVcf;
                        //updateChildItem.Temperature = smartContentsData.Temperature;
                        //updateChildItem.CoefficientWcf = smartContentsData.CoefficientWcf;
                        //updateChildItem.Density = smartContentsData.Density;
                        //updateChildItem.AmountExciseTax = smartContentsData.AmountExciseTax;
                        //updateChildItem.ExciseTaxRate = smartContentsData.ExciseTaxRate;
                        //updateChildItem.BogRate = smartContentsData.BogRate;
                        //updateChildItem.AmountBog = smartContentsData.AmountBog;
                        //updateChildItem.AmountTotal = smartContentsData.AmountTotal;
                        //updateChildItem.PriceEnd = smartContentsData.PriceEnd;
                        //updateChildItem.VoucherNumberContents = smartContentsData.VoucherNumberContents;
                        //updateChildItem.Notes = smartContentsData.Notes;
                        //updateChildItem.CodeUnit = smartContentsData.CodeUnit;
                        //updateChildItem.IsActive = smartContentsData.IsActive;
                        //updateChildItem.CreateDate = smartContentsData.CreateDate;
                        //updateChildItem.CreateBy = smartContentsData.CreateBy;
                        //updateChildItem.ModifyDate = smartContentsData.ModifyDate;
                        //updateChildItem.ModifyBy = smartContentsData.ModifyBy;
                        //updateChildItem.IdData = smartContentsData.IdData;
                        //updateChildItem.IdSource = smartContentsData.IdSource;
                        //updateChildItem.LOAIPHIEU = smartContentsData.LOAIPHIEU;
                        //updateChildItem.SignTransfer = smartContentsData.SignTransfer;
                        //updateChildItem.AmountOfMoney15 = smartContentsData.AmountOfMoney15;
                        //updateChildItem.CostOfGoodsSold15 = smartContentsData.CostOfGoodsSold15;
                        //updateChildItem.Season = smartContentsData.Season;
                        //updateChildItem.InvoiceDate = smartContentsData.InvoiceDate;
                        //updateChildItem.EnviromentByKg = smartContentsData.EnviromentByKg;
                        //updateChildItem.ShipmentNumber = smartContentsData.ShipmentNumber;
                        //updateChildItem.IdVouchers = smartContentsData.IdVouchers;
                        //updateChildItem.IdTracing = smartContentsData.IdTracing;
                        //updateChildItem.DateShipment = smartContentsData.DateShipment;
                        //updateChildItem.WarehoseData = smartContentsData.WarehoseData;
                        //updateChildItem.ImpTaxRate = smartContentsData.ImpTaxRate;
                        //updateChildItem.AmountImpTax = smartContentsData.AmountImpTax;
                        //updateChildItem.StorageLocationCode = smartContentsData.StorageLocationCode;
                        //updateChildItem.StorageLocationName = smartContentsData.StorageLocationName;
                        //updateChildItem.KeyWord = smartContentsData.KeyWord;

                        #endregion

                        services.Mapper.Map(smartContentsData, updateChildItem);
                    }
                    else
                    {
                        smartData.SmartContentsDatas.Add(smartContentsData);
                    }
                }
            }

            await services.Context.SaveChangesAsync();

            var response = await services.Context.SmartDatas
                .Include(x => x.SmartContentsDatas.OrderBy(y => y.CreateDate))
                .Include(x => x.SmartFileAttaches)
                .FirstOrDefaultAsync(x => x.Id == id);

            return TypedResults.Ok(ApiResponseFactory<SmartData>.Ok(response));

        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private static async Task<Results<Ok<ApiResponse<string>>, BadRequest<ApiResponse<string>>>> Delete(
        [AsParameters] VoucherServices services,
        Guid id)
    {
        try
        {
            var smartData = await services.Context.SmartDatas
                .Include(x => x.SmartContentsDatas.OrderBy(y => y.CreateDate))
                .Include(x => x.SmartFileAttaches)
                .SingleOrDefaultAsync(i => i.Id == id);

            services.Context.SmartDatas.Remove(smartData);

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(ApiResponseFactory<string>.NoContent());
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ApiResponseFactory<string>.BadRequest(ex.Message));
        }
    }

    private static async Task<Ok<Result>> FindOneSales(
        [AsParameters] VoucherServices services,
        [FromQuery] string? typeVoucher,
        [FromRoute] Guid id)
    {
        //var smartData = await services.Context.SalesSmartData
        //    //.Include(x => x.SalesSmartContentsDatas.OrderBy(y => y.CreateDate))
        //    .FirstOrDefaultAsync(x => x.Id == id);
        var connection = services.Context.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        var storedProcedure = new GetVoucherStoreProduce($"{typeVoucher}", "OrderSell", "", 100, "", id.ToString());

        var multipleResult = await connection.QueryMultipleAsync(storedProcedure.StoredProcedureName, storedProcedure.Parameters);
        var salesSmartData = await multipleResult.ReadSingleAsync<object>();
        var salesSmartContentData = await multipleResult.ReadAsync<object>();


        return TypedResults.Ok<Result>(Result.Success(new { salesSmartData, salesSmartContentData }));
    }
    private static async Task<Ok<ApiResponse<SmartDebtOffSet>>> FindOneDebtOffSet(
   [AsParameters] VoucherServices services,
   [FromRoute] Guid id)
    {
        var smartDebtOffSet = await services.Context.SmartDebtOffSets
            .Include(x => x.SmartDebtOffSetContents.OrderBy(y => y.CreateDate))
            .FirstOrDefaultAsync(x => x.Id == id);

        var response = ApiResponseFactory<SmartDebtOffSet>.Ok(smartDebtOffSet);

        return TypedResults.Ok(response);
    }

}
