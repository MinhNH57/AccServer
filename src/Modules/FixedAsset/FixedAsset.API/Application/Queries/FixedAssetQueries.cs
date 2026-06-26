using Newtonsoft.Json;

namespace FixedAsset.API.Application.Queries;

public class FixedAssetQueries(FixedAssetDbContext context, IMapper mapper)
    : IFixedAssetQueries
{
    private readonly FixedAssetDbContext _context = context;

    private readonly IMapper _mapper = mapper;

    public async Task<string> GetFixedAssetNextValueAsync(int categories, Guid? branchId)
    {
        try
        {
            string nextValue = await _context.Database
                .GetDbConnection()
                .QueryFirstAsync<string>("CodeNextValue", new { Categories = categories, BranchId = branchId });

            return nextValue;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public async Task<FixedAssetResponse> GetFixedAssetDetailFullAsync(string id, int type)
    {
        var fixedAsset = await _context.FixedAssets
            .AsNoTracking()
            .Include(x => x.FixedAssetDetailAllocations)
            .Include(x => x.FixedAssetDetailSources)
            .Include(x => x.FixedAssetDetails)
            .Include(x => x.FixedAssetDetailAccessories)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fixedAsset != null)
        {
            var existedConstrainIds = await _context.Database
                .GetDbConnection()
                .QueryAsync<Guid>("CheckExistedConstrain", new { RefIds = id, RefType = type });

            var fixedAssetDto = _mapper.Map<FixedAssetDto>(fixedAsset);
            fixedAssetDto.IsReferenced = existedConstrainIds.Any();

            var response = new FixedAssetResponse
            {
                FixedAsset = [fixedAssetDto],
                FixedAssetDetailAllocation = [.. fixedAsset.FixedAssetDetailAllocations.Select(_mapper.Map<FixedAssetDetailAllocationDto>)],
                FixedAssetDetailSource = [.. fixedAsset.FixedAssetDetailSources.Select(_mapper.Map<FixedAssetDetailSourceDto>)],
                FixedAssetDetail = [.. fixedAsset.FixedAssetDetails.Select(_mapper.Map<FixedAssetDetailDto>)],
                FixedAssetDetailAccessory = [.. fixedAsset.FixedAssetDetailAccessories.Select(_mapper.Map<FixedAssetDetailAccessoryDto>)]
            };

            return response;
        }

        return null;
    }

    public async Task<Pagination<FixedAssetDto>> GetFixedAssetsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FixedAssets.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:

                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset>(queryString, summaryColumns);

                return new Pagination<FixedAssetDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FixedAssetDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<Domain.AggregatesModel.FixedAssetAggregate.FixedAsset>(queryString, summaryColumns);

                return new Pagination<FixedAssetDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<FixedAssetDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FixedAssetDto>)],
                    Total = total,
                };
        }
    }

    public async Task<FADecrementResponse> GetFADecrementDetailFullAsync(string id, int type)
    {
        var fADecrement = await _context.FADecrements
            .AsNoTracking()
            .Include(x => x.FADecrementDetails)
            .Include(x => x.FADecrementDetailPosts)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fADecrement != null)
        {
            var response = new FADecrementResponse
            {
                FADecrement = [_mapper.Map<FADecrementDto>(fADecrement)],
                FADecrementDetail = [.. fADecrement.FADecrementDetails.Select(_mapper.Map<FADecrementDetailDto>)],
                FADecrementDetailPost = [.. fADecrement.FADecrementDetailPosts.Select(_mapper.Map<FADecrementDetailPostDto>)]
            };

            return response;
        }

        return null;
    }

    public async Task<Pagination<FADecrementDto>> GetFADecrementsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FADecrements.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:
                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<FADecrement>(queryString, summaryColumns);

                return new Pagination<FADecrementDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FADecrementDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<FADecrement>(queryString, summaryColumns);

                return new Pagination<FADecrementDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<FADecrementDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FADecrementDto>)],
                    Total = total,
                };
        }
    }

    public async Task<List<FADecrementAvailableFixedAsset>> GetFADecrementAvailableFixedAssetsAsync(DateTime postedDate, Guid? branchID = null, bool isManagementBook = false)
    {
        var availableFixedAssets = await _context.Database
          .GetDbConnection()
          .QueryAsync<FADecrementAvailableFixedAsset>("GetFADecrementAvailableFixedAsset", new { BranchID = branchID, PostedDate = postedDate, IsManagementBook = isManagementBook });

        return [.. availableFixedAssets];
    }

    public async Task<List<FATransferAvailableFixedAsset>> GetFATransferAvailableFixedAssetsAsync(DateTime postedDate, Guid? branchID = null, bool isManagementBook = false)
    {
        var availableFixedAssets = await _context.Database
          .GetDbConnection()
          .QueryAsync<FATransferAvailableFixedAsset>("GetFATransferAvailableFixedAsset", new { BranchID = branchID, PostedDate = postedDate, IsManagementBook = isManagementBook });

        return [.. availableFixedAssets];
    }

    public async Task<List<FADecrementCustomFixedAsset>> GetFADecrementCustomFixedAssetsAsync(string fixedAssetIDs, Guid? branchID = null, DateTime? postedDate = null, bool isManagementBook = false)
    {
        var customFixedAssets = await _context.Database
                .GetDbConnection()
                .QueryAsync<FADecrementCustomFixedAsset>("GetFADecrementCustomFixedAsset", new { FixedAssetIDs = fixedAssetIDs, BranchID = branchID, PostedDate = postedDate, IsManagementBook = isManagementBook });

        return [.. customFixedAssets];
    }

    public async Task<Pagination<FATransferDto>> GetFATransfersAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FATransfers.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:
                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<FATransfer>(queryString, summaryColumns);

                return new Pagination<FATransferDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FATransferDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<FATransfer>(queryString, summaryColumns);

                return new Pagination<FATransferDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<FATransferDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FATransferDto>)],
                    Total = total,
                };
        }
    }

    public async Task<FATransferResponse> GetFATransferDetailFullAsync(string id, int type)
    {
        var fATransfer = await _context.FATransfers
            .AsNoTracking()
            .Include(x => x.FATransferDetails)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fATransfer != null)
        {
            var response = new FATransferResponse
            {
                FATransfer = [_mapper.Map<FATransferDto>(fATransfer)],
                FATransferDetail = [.. fATransfer.FATransferDetails.Select(_mapper.Map<FATransferDetailDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<List<FADepreciationDto>> CheckExistsFADepreciationAsync(int month, int year)
    {
        var fADepreciation = await _context.FADepreciations.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Year == year && x.Month == month);

        if (fADepreciation == null) return [];

        return [fADepreciation.Adapt<FADepreciationDto>()];
    }

    public async Task<List<FAChangeFinancialLeasingToOwnerAvailableFixedAsset>> GetFAChangeFinancialLeasingToOwnerAvailableFixedAssetsAsync(DateTime toDate, Guid? fixedAssetId = null)
    {
        var availableFixedAssets = await _context.Database
          .GetDbConnection()
          .QueryAsync<FAChangeFinancialLeasingToOwnerAvailableFixedAsset>("GetFAChangeFinancialLeasingToOwnerAvailableFixedAsset", new { ToDate = toDate, FixedAssetId = fixedAssetId });

        return [.. availableFixedAssets];
    }

    public async Task<FADepreciationDetailResponse> GetFADepreciationDetailAsync(DateTime postedDate)
    {
        var multi = await _context.Database
            .GetDbConnection()
            .QueryMultipleAsync("GetFADepreciationDetail", new { PostedDate = postedDate });

        if (multi != null)
        {
            var response = new FADepreciationDetailResponse
            {
                FADepreciationDetail = [.. await multi.ReadAsync<FADepreciationDetailDto>()],
                FADepreciationDetailAllocation = [.. await multi.ReadAsync<FADepreciationDetailAllocationDto>()],
            };

            return response;
        }

        return null;
    }

    public async Task<FADepreciationResponse> GetFADepreciationDetailFullAsync(string id, int type)
    {
        var fADepreciation = await _context.FADepreciations
            .AsNoTracking()
            .Include(x => x.FADepreciationDetails)
            .Include(x => x.FADepreciationDetailAllocations)
            .Include(x => x.FADepreciationDetailPosts)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fADepreciation != null)
        {
            var response = new FADepreciationResponse
            {
                FADepreciation = [_mapper.Map<FADepreciationDto>(fADepreciation)],
                FADepreciationDetail = [.. fADepreciation.FADepreciationDetails.Select(_mapper.Map<FADepreciationDetailDto>)],
                FADepreciationDetailAllocation = [.. fADepreciation.FADepreciationDetailAllocations.Select(_mapper.Map<FADepreciationDetailAllocationDto>)],
                FADepreciationDetailPost = [.. fADepreciation.FADepreciationDetailPosts.Select(_mapper.Map<FADepreciationDetailPostDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<Pagination<FAAdjustmentDto>> GetFAAdjustmentsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FAAdjustments.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:
                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<FAAdjustment>(queryString, summaryColumns);

                return new Pagination<FAAdjustmentDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FAAdjustmentDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<FAAdjustment>(queryString, summaryColumns);

                return new Pagination<FAAdjustmentDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<FAAdjustmentDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FAAdjustmentDto>)],
                    Total = total,
                };
        }
    }

    public async Task<FAAdjustmentResponse> GetFAAdjustmentDetailFullAsync(string id, int type)
    {
        var fAAdjustment = await _context.FAAdjustments
            .AsNoTracking()
            .Include(x => x.FAAdjustmentDetails)
            .Include(x => x.FAAdjustmentDetailPosts)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fAAdjustment != null)
        {
            var response = new FAAdjustmentResponse
            {
                FAAdjustment = [_mapper.Map<FAAdjustmentDto>(fAAdjustment)],
                FAAdjustmentDetail = [.. fAAdjustment.FAAdjustmentDetails.Select(_mapper.Map<FAAdjustmentDetailDto>)],
                FAAdjustmentDetailPost = [.. fAAdjustment.FAAdjustmentDetailPosts.Select(_mapper.Map<FAAdjustmentDetailPostDto>)]
            };

            return response;
        }

        return null;
    }

    public async Task<FAAuditResponse> GetFAAuditDetailFullAsync(string id, int type)
    {
        var fAAudit = await _context.FAAudits
            .AsNoTracking()
            .Include(x => x.FAAuditDetails)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fAAudit != null)
        {
            var response = new FAAuditResponse
            {
                FAAudit = [_mapper.Map<FAAuditDto>(fAAudit)],
                FAAuditDetail = [.. fAAudit.FAAuditDetails.Select(_mapper.Map<FAAuditDetailDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<List<FAAdjustmentAvailableFixedAsset>> GetFAAdjustmentAvailableFixedAssetsAsync(DateTime postedDate, Guid? branchID = null, bool isManagementBook = false)
    {
        var availableFixedAssets = await _context.Database
          .GetDbConnection()
          .QueryAsync<FAAdjustmentAvailableFixedAsset>("GetFAAdjustmentAvailableFixedAsset", new { BranchID = branchID, PostedDate = postedDate, IsManagementBook = isManagementBook });

        return [.. availableFixedAssets];
    }

    public async Task<List<FAAuditAvailableFixedAsset>> GetFAAuditAvailableFixedAssetsAsync(DateTime auditDate)
    {
        var availableFixedAssets = await _context.Database
          .GetDbConnection()
          .QueryAsync<FAAuditAvailableFixedAsset>("GetFAAuditAvailableFixedAsset", new { AuditDate = auditDate });

        return [.. availableFixedAssets];
    }

    public async Task<List<FAAdjustmentCustomFixedAsset>> GetFAAdjustmentCustomFixedAssetsAsync(string fixedAssetIDs, Guid? branchID = null, DateTime? postedDate = null, bool isManagementBook = false)
    {
        var customFixedAssets = await _context.Database
        .GetDbConnection()
        .QueryAsync<FAAdjustmentCustomFixedAsset>("GetFAAdjustmentCustomFixedAsset", new { FixedAssetIDs = fixedAssetIDs, BranchID = branchID, PostedDate = postedDate, IsManagementBook = isManagementBook });

        return [.. customFixedAssets];
    }

    public async Task<Pagination<FADepreciationDto>> GetFADepreciationAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FADepreciations.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:
                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<FADepreciation>(queryString, summaryColumns);

                return new Pagination<FADepreciationDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FADepreciationDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<FADepreciation>(queryString, summaryColumns);

                return new Pagination<FADepreciationDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<FADepreciationDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FADepreciationDto>)],
                    Total = total,
                };
        }
    }

    public async Task<List<RelatedVoucher>> GetFAArisingAsync(Guid refId, int refType, int displayOnBook)
    {
        var relatedVouchers = await _context.Database
        .GetDbConnection()
        .QueryAsync<RelatedVoucher>("GetRelatedVoucher", new { RefId = refId, RefType = refType, DisplayOnBook = displayOnBook });

        return [.. relatedVouchers];
    }

    public async Task<Pagination<FAChangeFinancialLeasingToOwnerDto>> GetFAChangeFinancialLeasingToOwnersAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FAChangeFinancialLeasingToOwners.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:
                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<FAChangeFinancialLeasingToOwner>(queryString, summaryColumns);

                return new Pagination<FAChangeFinancialLeasingToOwnerDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FAChangeFinancialLeasingToOwnerDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<FAChangeFinancialLeasingToOwner>(queryString, summaryColumns);

                return new Pagination<FAChangeFinancialLeasingToOwnerDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

                return new Pagination<FAChangeFinancialLeasingToOwnerDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FAChangeFinancialLeasingToOwnerDto>)],
                    Total = total,
                };
        }
    }

    public async Task<FAChangeFinancialLeasingToOwnerResponse> GetFAChangeFinancialLeasingToOwnerDetailFullAsync(string id, int type)
    {
        var fAChangeFinancialLeasingToOwner = await _context.FAChangeFinancialLeasingToOwners
            .AsNoTracking()
            .Include(x => x.FAChangeFinancialLeasingToOwnerDetails)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (fAChangeFinancialLeasingToOwner != null)
        {
            var response = new FAChangeFinancialLeasingToOwnerResponse
            {
                FAChangeFinancialLeasingToOwner = [_mapper.Map<FAChangeFinancialLeasingToOwnerDto>(fAChangeFinancialLeasingToOwner)],
                FAChangeFinancialLeasingToOwnerDetail = [.. fAChangeFinancialLeasingToOwner.FAChangeFinancialLeasingToOwnerDetails.Select(_mapper.Map<FAChangeFinancialLeasingToOwnerDetailDto>)],
            };

            return response;
        }

        return null;
    }


    public async Task<Pagination<FAAuditDto>> GetFAAuditsAsync(string sort, List<Filter> filters, int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = _context.FAAudits.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            List<Sort> sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
            if (sort.Length > 0) query = query.ApplySort(sorts);
        }

        var total = await query.CountAsync();

        switch (loadMode)
        {
            case (int)EnumPagingDataType.All:
                var pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var summaryData = await _context.GetSummaryAsync<FAAudit>(queryString, summaryColumns);

                return new Pagination<FAAuditDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FAAuditDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await _context.GetSummaryAsync<FAAudit>(queryString, summaryColumns);

                return new Pagination<FAAuditDto>()
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Data:
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<FAAuditDto>()
                {
                    PageData = [.. pageData.Select(_mapper.Map<FAAuditDto>)],
                    Total = total,
                };
        }
    }

    public async Task<List<FAAuditCheckExistFADecrementToFAAudit>> CheckExistFADecrementToFAAuditAsync(string ids)
    {
        var fAAuditCheckExistFADecrementToFAAudits = await _context.Database
            .GetDbConnection()
            .QueryAsync<FAAuditCheckExistFADecrementToFAAudit>("CheckExistFADecrementToFAAudit", new { FixedAssetIDs = ids });

        return [.. fAAuditCheckExistFADecrementToFAAudits];
    }

    public async Task<List<GetDataOriginalResponse>> GetFADecrementSUOriginalFADecrementAsync(DateTime fromDate, DateTime toDate, int refType, List<Guid> listRefId)
    {
        var originals = await _context.Database
          .GetDbConnection()
          .QueryAsync<GetDataOriginalResponse>("GetFADecrementSUOriginal", new { FromDate = fromDate, ToDate = toDate, RefType = refType, ListRefId = string.Join(",", listRefId) });

        return [.. originals];
    }

    public async Task<List<SelectSUFromFADecrement>> GetFADecrementGetSelectSUFromFADecrementAsync(DateTime fromDate, DateTime toDate)
    {
        var sus = await _context.Database
          .GetDbConnection()
          .QueryAsync<SelectSUFromFADecrement>("GetSelectSUFromFADecrement", new { FromDate = fromDate, ToDate = toDate });

        return [.. sus];
    }
}
