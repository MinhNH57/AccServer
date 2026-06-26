using BuildingBlocks.CQRS;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Voucher.Acc.Infrastructure;
using Voucher.Acc.Model;

namespace Voucher.Acc.Features.Commands.ManufactureApi.Update;

public class UpdateManufactureCommandHandler(
    VoucherDbContext dbContext,
    IMultiTenantContextAccessor tenantContextAccessor,
    IMapper mapper,
    ICurrentUser currentUser) : ICommandHandler<UpdateManufactureCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateManufactureCommand command, CancellationToken cancellationToken)
    {
        var smartData = await dbContext.SmartDataManufactures.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.Request.Id, cancellationToken: cancellationToken);

        if (smartData is null)
            return Result.Failure<bool>(new Error("404", "Not found"));

        var lstContent = await dbContext.SmartDataManufactureContents
            .AsNoTracking()
            .Where(c => c.IdContents == command.Request.Id)
            .ToListAsync(cancellationToken: cancellationToken);
        var lstContentBillOfMaterial = await dbContext.SmartDataBillOfMaterials
          .AsNoTracking()
          .Where(c => c.IdContents == command.Request.Id)
          .ToListAsync(cancellationToken: cancellationToken);
        var lstUpdate = command.Request.SmartDataManufacture.SmartDataManufactureContents
            .Where(c => lstContent.Any(r => r.IdSource == c.IdSource))
            .ToList();
        var lstUpdateBillOfMaterial = command.Request.SmartDataManufacture.SmartDataBillOfMaterials
           .Where(c => lstContentBillOfMaterial.Any(r => r.IdSource == c.IdSource))
           .ToList();

        var lstCreate = command.Request.SmartDataManufacture.SmartDataManufactureContents
            .Where(x => lstContent.All(y => y.IdSource != x.IdSource))
            .ToList();
        var lstCreateBillOfMaterial = command.Request.SmartDataManufacture.SmartDataBillOfMaterials
            .Where(x => lstContentBillOfMaterial.All(y => y.IdSource != x.IdSource))
            .ToList();

        var lstRemove = lstContent
            .Where(y => command.Request.SmartDataManufacture.SmartDataManufactureContents.All(x => x.IdSource != y.IdSource))
            .ToList();
        var lstRemoveBillOfMaterial = lstContentBillOfMaterial
            .Where(y => command.Request.SmartDataManufacture.SmartDataBillOfMaterials.All(x => x.IdSource != y.IdSource))
            .ToList();

        smartData = mapper.Map<SmartDataManufacture>(command.Request.SmartDataManufacture);
        smartData.ModifyBy = currentUser.CodeUser;
        smartData.ModifyDate = DateTime.Now;
        smartData.SmartDataManufactureContents.Clear();
        smartData.SmartDataBillOfMaterials.Clear();
        dbContext.SmartDataManufactures.Update(smartData);
        lstContent = mapper.Map<List<SmartDataManufactureContents>>(lstUpdate);
        lstContentBillOfMaterial = mapper.Map<List<SmartDataBillOfMaterials>>(lstUpdateBillOfMaterial);
        lstCreate.ForEach(c => c.IdContents = command.Request.Id);
        lstCreateBillOfMaterial.ForEach(c => c.IdContents = command.Request.Id);
        await dbContext.SmartDataManufactureContents.AddRangeAsync(lstCreate, cancellationToken);
        await dbContext.SmartDataBillOfMaterials.AddRangeAsync(lstCreateBillOfMaterial, cancellationToken);
        dbContext.SmartDataManufactureContents.UpdateRange(lstContent);
        dbContext.SmartDataManufactureContents.RemoveRange(lstRemove);
        dbContext.SmartDataBillOfMaterials.UpdateRange(lstContentBillOfMaterial);
        dbContext.SmartDataBillOfMaterials.RemoveRange(lstRemoveBillOfMaterial);
        var rsl = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        return Result.Success(true);
    }

}