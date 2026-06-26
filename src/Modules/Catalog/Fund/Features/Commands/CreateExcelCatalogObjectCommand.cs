using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Catalog.Fund.Entities;
using Catalog.Fund.Infrastructure;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Fund.Features.Commands;

public record CreateExcelCatalogObjectCommand(ExcelCatalogObject ExcelCatalogObjects) : ICommand<Result>;

public class CreateExcelCatalogObjectCommandHandler(
    CatalogFundContext dbContext,
    ICurrentUser currentUser,
    SmartDataServices dataServices,
    IMapper mapper) : ICommandHandler<CreateExcelCatalogObjectCommand, Result>
{
    public async Task<Result> Handle(CreateExcelCatalogObjectCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (request.ExcelCatalogObjects.Id == Guid.Empty)
        {
            await CreateData(request.ExcelCatalogObjects, cancellationToken);
            return Result.Success("Create data successfully");
        }

        await UpdateData(request.ExcelCatalogObjects, cancellationToken);
        return Result.Success("Update data successfully");
    }

    //Sinh mã thành viên theo CĐCT
    private string GenneralUnitCode(int stt)
    {
        string rsl = string.Empty;

        switch (stt.ToString().Length)
        {
            case 1:
                rsl = "000" + stt;
                break;
            case 2:
                rsl = "00" + stt;
                break;

            case 3:
                rsl = "0" + stt;
                break;
            case 4:
                rsl = stt.ToString();
                break;
        }

        return rsl;
    }

    private async Task CreateData(ExcelCatalogObject excelCatalogObjects, CancellationToken token)
    {
        string strCodeWards = excelCatalogObjects.CodeWards;

        var data = await dbContext.CatalogObject.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CitizenIDNumber == excelCatalogObjects.CitizenIDNumber, cancellationToken: token);

        if (data is not null)
        {
            excelCatalogObjects.ObjCode = data.ObjCode;
        }
        else
        {
            int i = await dataServices
                .GetSingleObject<int>(
                    $"select [dbo].[GetMaxMemberByWards] ('CreateMemberCode', 'Id', N'ADMIN', 888, '{strCodeWards}','SmartTable' )",
                    dbContext.Database.GetConnectionString()!);

            var objCode = strCodeWards + GenneralUnitCode(i);

            excelCatalogObjects.ObjCode = objCode;
        }

        string numberImport = string.Empty;
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        if (string.IsNullOrEmpty(currentUser.CodeUser))
        {
            numberImport = $"CataObj-{timestamp}-{888}-{excelCatalogObjects.GrpCode}";
        }
        else
        {
            numberImport = $"CataObj-{timestamp}-{currentUser.CodeUnit}-{currentUser.CodeUser}";
        }
        excelCatalogObjects.NumberImport = numberImport;
        excelCatalogObjects.CreateBy = string.IsNullOrEmpty(currentUser.NameUser) ? excelCatalogObjects.ObjName : currentUser.NameUser;
        excelCatalogObjects.CreateDate = DateTime.Now;
        excelCatalogObjects.CodeManager = currentUser.CodeUser;
        excelCatalogObjects.NameManager = currentUser.NameUser;
        excelCatalogObjects.Register = true;
        await dbContext.ExcelCatalogObject.AddAsync(excelCatalogObjects, token);
        await dbContext.SaveChangesAsync(token);
    }

    private async Task UpdateData(ExcelCatalogObject excelCatalogObjects, CancellationToken token)
    {
        var data = await dbContext.ExcelCatalogObject
            .FirstOrDefaultAsync(c => c.Id == excelCatalogObjects.Id,
              cancellationToken: token);

        if (data is null)
            throw new NotFoundException(nameof(ExcelCatalogObject), excelCatalogObjects.Id);

        MapDTO(data, excelCatalogObjects);
        //mapper.Map(excelCatalogObjects, data);

        dbContext.ExcelCatalogObject.Update(data);
        await dbContext.SaveChangesAsync(token);
    }

    private void MapDTO(ExcelCatalogObject data, ExcelCatalogObject excelCatalogObjects)
    {
        data.GrpCode = excelCatalogObjects.GrpCode;
        data.GrpName = excelCatalogObjects.GrpName;
        data.CodeManager = excelCatalogObjects.CodeManager;
        data.NameManager = excelCatalogObjects.NameManager;
        data.ContractValue = excelCatalogObjects.ContractValue;
        data.Income = excelCatalogObjects.Income;
        data.ObjCode = excelCatalogObjects.ObjCode;
        data.ObjName = excelCatalogObjects.ObjName;
        data.PhoneNumber = excelCatalogObjects.PhoneNumber;
        data.Email = excelCatalogObjects.Email;
        data.AccountNumber = excelCatalogObjects.AccountNumber;
        data.BankName = excelCatalogObjects.BankName;
        data.ObjSex = excelCatalogObjects.ObjSex;
        data.CitizenIDNumber = excelCatalogObjects.CitizenIDNumber;
        data.PermanentAddress = excelCatalogObjects.PermanentAddress;
        data.MaritalStatus = excelCatalogObjects.MaritalStatus;
        data.GrantedBy = excelCatalogObjects.GrantedBy;
        data.ModifyDate = DateTime.Now;
        data.RestMoneyDebt = excelCatalogObjects.RestMoneyDebt;
        data.BadDebtBalance = excelCatalogObjects.BadDebtBalance;
        data.CreditProductCode = excelCatalogObjects.CreditProductCode;
        data.CreditProductName = excelCatalogObjects.CreditProductName;
        data.PurposeCode = excelCatalogObjects.PurposeCode;
        data.PurposeName = excelCatalogObjects.PurposeName;
        data.DateOfBirth = excelCatalogObjects.DateOfBirth;
        data.DisbursementFormCode = excelCatalogObjects.DisbursementFormCode;
        data.DisbursementFormName = excelCatalogObjects.DisbursementFormName;
        data.FundingSourceCode = excelCatalogObjects.FundingSourceCode;
        data.FundingSourceName = excelCatalogObjects.FundingSourceName;
        data.IsEmergency = excelCatalogObjects.IsEmergency;
        data.IsInsurance = excelCatalogObjects.IsInsurance;
        data.CreditPeriod = excelCatalogObjects.CreditPeriod;
        data.CreateJobs = excelCatalogObjects.CreateJobs;
        data.GuarantorNameJob = excelCatalogObjects.GuarantorNameJob;
        data.MonthPeriod = excelCatalogObjects.MonthPeriod;
        data.PurposeCredit = excelCatalogObjects.PurposeCredit;
    }
}