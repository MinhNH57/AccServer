using Microsoft.Extensions.Logging;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Catalogs.Object;
using SmartAccCloud.Application.Pagination;
using System.Text.Json;
using AutoMapper.QueryableExtensions;
using SmartAccCloud.Application.Extensions;
using SmartAccCloud.Application.Services.FileWork;
using SmartAccCloud.Application.Caching;
using FirebaseAdmin.Auth.Multitenancy;
using SmartAccCloud.Application.Models.Users;
using Finbuckle.MultiTenant.Abstractions;
using SmartAccCloud.Application.Commons.Enums;
using SmartAccCloud.Application.Models.Catalogs.SurveyExpertise;

namespace SmartAccCloud.Application.Services.Catalogs.CatalogObject;

public class CatalogObjectServices(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser, ILogger<CatalogObjectServices> logger, IFileAttachServices fileAttachServices, IRedisCacheService cache, IMultiTenantContextAccessor tenantContextAccessor) : ICatalogObjectServices
{
    private readonly TenantInfoCustomize? _tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
    public async Task<Result<bool>> CreateObject(Domain.Entity.Catalogs.CatalogObject param)
    {
        bool existsByCode = await context.CatalogObject.AsNoTracking()
            .AnyAsync(x => x.ObjCode == param.ObjCode)
            .ConfigureAwait(false);
        if (existsByCode)
            return Result<bool>.Failure(new Error("400", "Mã đối tượng đã tồn tại"));
        if (!string.IsNullOrWhiteSpace(param.TaxCode))
        {
            var existsByTax = await context.CatalogObject.AsNoTracking()
                .FirstOrDefaultAsync(x => x.TaxCode == param.TaxCode)
                .ConfigureAwait(false);
            if (existsByTax != null)
                return Result<bool>.Failure(new Error("400", $"Mã số thuế đã tồn tại ở đối tượng {existsByTax.ObjCode}"));
        }
        try
        {
            await context.CatalogObject.AddAsync(param).ConfigureAwait(false);
            int count = await context.SaveChangesAsync().ConfigureAwait(false);

            if (count > 0)
            {
                await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(new Error("400", "Thêm thất bại"));
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(new Error("500", e.Message));
        }
    }
    private string GetCatalogCacheKey(string id)
    {
        return $"KT:catalog:{_tenant.Identifier}:{id}";
    }

    public async Task<Result<bool>> EditObject(ObjectDto param)
    {
        var objectEdit = await context.CatalogObject
            .FirstOrDefaultAsync(x => x.ObjCode == param.ObjCode).ConfigureAwait(false);
        if (objectEdit == null)
            return Result<bool>.Failure(new Error("400", "Sửa thất bại do mã đối tượng không tồn tại"));
        if (!string.IsNullOrWhiteSpace(param.TaxCode))
        {
            var existsByTax = await context.CatalogObject.AsNoTracking()
                .FirstOrDefaultAsync(x => x.TaxCode == param.TaxCode && x.ObjCode != param.ObjCode)
                .ConfigureAwait(false);
            if (existsByTax != null)
            {
                await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result<bool>.Failure(new Error("400", $"Mã số thuế đã tồn tại ở đối tượng {existsByTax.ObjCode}"));
            }

        }
        try
        {
            objectEdit = mapper.Map(param, objectEdit);

            context.CatalogObject.Update(objectEdit);
            int count = await context.SaveChangesAsync().ConfigureAwait(false);

            if (count > 0)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(new Error("400", "Cập nhật thất bại"));
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(new Error("500", e.Message));
        }
    }

    public async Task<Result<Domain.Entity.Catalogs.CatalogObject>> GetObjectByCccd(string cccd)
    {
        var obj = await context.CatalogObject.FirstOrDefaultAsync(x => x.CitizenIDNumber == cccd.Trim());
        if (obj is not null)
        {
            return Result<Domain.Entity.Catalogs.CatalogObject>.Success(obj);
        }
        return Result<Domain.Entity.Catalogs.CatalogObject>.Failure(new("400", "Chưa có đối tượng có mã căn cước này"));
    }

    public async Task<Result<ObjectViewMobileDetail>> GetObjectByCccdDetail(string cccd)
    {
        var obj = await context.CatalogObject.AsNoTracking().FirstOrDefaultAsync(x => x.CitizenIDNumber == cccd.Trim())
            .ConfigureAwait(false);
        if (obj is not null)
        {

            var lstRelationShip = await context.CatalogRelationship.Where(x => x.IdMember == obj.Id).
                AsNoTracking().ToListAsync().ConfigureAwait(false);


            var surveyExpertise = await context.SurveyExpertise.FirstOrDefaultAsync(x => x.IdSource == obj.Id).ConfigureAwait(false);

            var objMap = mapper.Map<ObjectDtoFund>(obj);
            var surveyMap = mapper.Map<SurveyExpertiseDto>(surveyExpertise);

            var lstRelationShipMap = mapper.Map<List<CatalogRelationshipDto>>(lstRelationShip);

            return Result<ObjectViewMobileDetail>.Success(new() { Object = objMap, LstRelationShip = lstRelationShipMap ?? [], SurveyExpertise = surveyMap });
        }
        return Result<ObjectViewMobileDetail>.Failure(new("400", "Chưa có đối tượng có mã căn cước này"));

    }

    public async Task<Result<PagedResult<ObjectViewMobile>>> GetListMobile(PaginationRequest request, CancellationToken token)
    {
        var data = await context.CatalogObject
            .ProjectTo<ObjectViewMobile>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .PaginateAsync(request, token);

        // List<ObjectViewMobile> viewMobile = new();

        // viewMobile = mapper.Map(lstGet, viewMobile);

        return Result<Pagination.PagedResult<ObjectViewMobile>>.Success(data);
    }

    public async Task<Result<bool>> RemoveObjectByCccd(string cccd)
    {
        var obj = await context.CatalogObject.FirstOrDefaultAsync(x => x.CitizenIDNumber == cccd.Trim());
        if (obj is not null)
        {
            context.CatalogObject.Remove(obj);
            int count = await context.SaveChangesAsync().ConfigureAwait(false);

            if (count > 0)
            {
                var relationShip = await context.CatalogRelationship.Where(x => x.IdMember == obj.Id)
                    .AsNoTracking().ToListAsync().ConfigureAwait(false);
                context.CatalogRelationship.RemoveRange(relationShip);

                await fileAttachServices.DeleteAllFileFund(new()
                { ColumnTable = "CatalogObject", KeyTable = obj.Id.ToString() });

                var surveyExpertise = await context.SurveyExpertise.FirstOrDefaultAsync(x => x.IdSource == obj.Id);
                if (surveyExpertise is not null)
                {
                    context.SurveyExpertise.Remove(surveyExpertise);
                }

                logger.LogInformation("User: '{userName}' removed data with type: 'CatalogObject' with cccd: '{cccd}' .", currentUser.CodeUser, cccd);
                await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result<bool>.Success(true);
            }
            logger.LogInformation("User: '{userName}' failed to remove data with type: 'CatalogObject' with cccd: '{cccd}' .", currentUser.CodeUser, cccd);
            return Result<bool>.Failure(new Error("400", "Xoá thất bại"));
        }
        return Result<bool>.Failure(new("400", "Chưa có đối tượng có mã căn cước này"));
    }

    public async Task<Result<bool>> CreateObjectFund(ObjectDtoFundAction param)
    {
        bool existsByCode = await context.CatalogObject.AsNoTracking()
            .AnyAsync(x => x.CitizenIDNumber == param.Object.CitizenIDNumber.Trim())
            .ConfigureAwait(false);
        Domain.Entity.Catalogs.CatalogObject objCreate = new();
        if (existsByCode)
            return Result<bool>.Failure(new Error("400", "Căn cước đã tồn tại trong hệ thống"));
        try
        {
            param.Object.ObjCode = param.Object.CitizenIDNumber;
            objCreate = mapper.Map(param.Object, objCreate);
            objCreate.CodeUnit = currentUser.CodeUnit;

            //khảo sát
            if (param.SurveyExpertise is not null)
            {
                SurveyExpertise surveyCreate = new();
                surveyCreate = mapper.Map(param.SurveyExpertise, surveyCreate);
                surveyCreate.IdSource = param.Object.Id;
                surveyCreate.CreatedBy = currentUser.CodeUser;
                surveyCreate.LastModifiedBy = currentUser.CodeUser;

                await context.SurveyExpertise.AddAsync(surveyCreate).ConfigureAwait(false);
            }

            List<CatalogRelationship> lstRelationshipsCreate = new();

            lstRelationshipsCreate = mapper.Map(param.LstRelationShip, lstRelationshipsCreate);
            foreach (var item in lstRelationshipsCreate)
            {
                item.IdMember = objCreate.Id;
                item.MemberCode = objCreate.Id.ToString();
                item.CodeUnit = objCreate.CodeUnit;
            }
            await context.CatalogObject.AddAsync(objCreate).ConfigureAwait(false);
            await context.CatalogRelationship.AddRangeAsync(lstRelationshipsCreate).ConfigureAwait(false);


            int count = await context.SaveChangesAsync().ConfigureAwait(false);

            string objectJson = JsonSerializer.Serialize(objCreate);
            string relationshipJson = JsonSerializer.Serialize(lstRelationshipsCreate);
            if (count > 0)
            {
                logger.LogInformation("User: '{userName}' created data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);
                logger.LogInformation("User: '{userName}' created data with type: 'CatalogRelationship' content: '{json}'.", currentUser.CodeUser, relationshipJson);
                await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result<bool>.Success(true);
            }
            logger.LogInformation("User: '{userName}'  failed to create data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);
            logger.LogInformation("User: '{userName}' failed to create data with type: 'CatalogRelationship' content: '{json}'.", currentUser.CodeUser, relationshipJson);


            return Result<bool>.Failure(new Error("400", "Thêm thất bại"));
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(new Error("500", e.Message));
        }
    }

    public async Task<Result<bool>> UpdateObjectFund(ObjectDtoFundAction param)
    {
        var objectEdit = await context.CatalogObject
            .FirstOrDefaultAsync(x => x.CitizenIDNumber == param.Object.CitizenIDNumber).ConfigureAwait(false);
        if (objectEdit == null)
            return Result<bool>.Failure(new Error("400", "Sửa thất bại do không có đối tượng chứa căn cước này."));
        try
        {
            objectEdit = mapper.Map(param.Object, objectEdit);

            //Khảo sát
            if (param.SurveyExpertise is not null)
            {
                var surveyEdit = await context.SurveyExpertise
                    .FirstOrDefaultAsync(x => x.IdSource == param.SurveyExpertise.Id).ConfigureAwait(false);

                if (surveyEdit is not null)
                {

                    surveyEdit = mapper.Map(param.SurveyExpertise, surveyEdit);
                    surveyEdit.LastModified = DateTime.Now;
                    surveyEdit.LastModifiedBy = currentUser.CodeUser;
                    context.SurveyExpertise.Update(surveyEdit);
                }
            }

            var relationShipCreate = param.LstRelationShip.Where(x => x.Type == StatusModel.Add);
            var relationShipUpdate = param.LstRelationShip.Where(x => x.Type == StatusModel.Update);
            var relationShipRemove = param.LstRelationShip.Where(x => x.Type == StatusModel.Delete);

            // Xoá danh sách quan hệ
            if (relationShipCreate != null)
            {
                // Tạo mới danh sách quan hệ
                var lstRelationshipsCreate = mapper.Map<List<CatalogRelationship>>(relationShipCreate)
                .Select(x =>
                {
                    x.IdMember = objectEdit.Id;
                    x.MemberCode = objectEdit.Id.ToString();
                    x.CodeUnit = objectEdit.CodeUnit;
                    return x;
                })
                .ToList();
                await context.CatalogRelationship.AddRangeAsync(lstRelationshipsCreate).ConfigureAwait(false);
            }
            // Cập nhật danh sách quan hệ
            if (relationShipUpdate != null)
            {
                var relationshipIdsToUpdate = relationShipUpdate.Select(x => x.Id).ToList();
                var existingRelationships = await context.CatalogRelationship
                    .Where(x => relationshipIdsToUpdate.Contains(x.Id))
                    .ToDictionaryAsync(x => x.Id)
                    .ConfigureAwait(false);

                var lstRelationshipsUpdate = relationShipUpdate
                    .Where(x => existingRelationships.ContainsKey(x.Id))
                    .Select(x => mapper.Map(x, existingRelationships[x.Id]))
                    .ToList();
                context.CatalogRelationship.UpdateRange(lstRelationshipsUpdate);
            }
            // Xóa danh sách quan hệ
            if (relationShipRemove != null)
            {
                var relationshipIdsToRemove = relationShipRemove
                    .Select(x => x.Id)
                    .ToList();

                var lstRelationshipsRemove = await context.CatalogRelationship
                    .Where(x => relationshipIdsToRemove.Contains(x.Id))
                    .ToListAsync()
                    .ConfigureAwait(false);

                context.CatalogRelationship.RemoveRange(lstRelationshipsRemove);

            }

            context.CatalogObject.Update(objectEdit);
            int count = await context.SaveChangesAsync().ConfigureAwait(false);
            string objectJson = JsonSerializer.Serialize(objectEdit);
            if (count > 0)
            {
                logger.LogInformation("User: '{userName}' updated data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson);
                await cache.RemoveByPatternAsync(GetCatalogCacheKey("CatalogObject"));
                return Result<bool>.Success(true);
            }
            logger.LogInformation("User: '{userName}'  failed to update data with type: 'CatalogObject' content: '{json}'.", currentUser.CodeUser, objectJson); return Result<bool>.Failure(new Error("400", "Cập nhật thất bại"));
        }
        catch (Exception e)
        {
            return Result<bool>.Failure(new Error("500", e.Message));
        }
    }
}