using Newtonsoft.Json;

namespace Supply.API.Application.Queries;

public class SupplyQueries(SupplyDbContext context, IMapper mapper)
    : ISupplyQueries
{
    public async Task<string> GetSUIncrementNextValueAsync(int categories, Guid? branchId)
    {
        try
        {
            var nextValue = await context.Database
                .GetDbConnection()
                .QueryFirstAsync<string>("CodeNextValue", new { Categories = categories, BranchId = branchId });

            return nextValue;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public async Task<SUIncrementResponse> GetSUIncrementDetailFullAsync(string id, int type)
    {
        var suIncrement = await context.SUIncrements
            .Include(x => x.SUIncrementDetailDepartments)
            .Include(x => x.SUIncrementDetailAllocations)
            .Include(x => x.SUIncrementDetails)
            .Include(x => x.SUIncrementDetailSources)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);
        if (suIncrement != null)
        {
            var existedConstrainIds = await context.Database
                .GetDbConnection()
                .QueryAsync<Guid>("CheckExistedConstrain", new { RefIds = id, RefType = type });

            var sUIncrementDto = mapper.Map<SUIncrementDto>(suIncrement);
            sUIncrementDto.IsReferenced = existedConstrainIds.Any();

            var response = new SUIncrementResponse
            {
                SUIncrement = [sUIncrementDto],
                SUIncrementDetailAllocation =
                    [.. suIncrement.SUIncrementDetailAllocations.Select(mapper.Map<SUIncrementDetailAllocationDto>)],
                SUIncrementDetailSource =
                    [.. suIncrement.SUIncrementDetailSources.Select(mapper.Map<SUIncrementDetailSourceDto>)],
                SUIncrementDetail = [.. suIncrement.SUIncrementDetails.Select(mapper.Map<SUIncrementDetailDto>)],
                SUIncrementDetailDepartment =
                    [.. suIncrement.SUIncrementDetailDepartments.Select(mapper.Map<SUIncrementDetailDepartmentDto>)]
            };

            return response;
        }

        return null;
    }

    public async Task<Pagination<SULedger>> GetSupplyLedgersAsync(string sort, List<Filter> filters, int pageIndex,
        int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SULedgers.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SULedger>(queryString, summaryColumns);

                return new Pagination<SULedger>
                {
                    PageData = pageData,
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SULedger>(queryString, summaryColumns);

                return new Pagination<SULedger>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SULedger>
                {
                    PageData = pageData,
                    Total = total,
                };
        }
    }

    public async Task<Pagination<SUIncrementDto>> GetSUIncrementsAsync(string sort, List<Filter> filters, int pageIndex,
        int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SUIncrements.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SUIncrement>(queryString, summaryColumns);

                return new Pagination<SUIncrementDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUIncrementDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SUIncrement>(queryString, summaryColumns);

                return new Pagination<SUIncrementDto>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SUIncrementDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUIncrementDto>)],
                    Total = total,
                };
        }
    }

    public async Task<SUDecrementResponse> GetSUDecrementDetailFullAsync(string id, int type)
    {
        var suDecrement = await context.SUDecrements
            .AsNoTracking()
            .Include(x => x.SUDecrementDetails)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (suDecrement != null)
        {
            var response = new SUDecrementResponse
            {
                SUDecrement = [mapper.Map<SUDecrementDto>(suDecrement)],
                SUDecrementDetail = [.. suDecrement.SUDecrementDetails.Select(mapper.Map<SUDecrementDetailDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<Pagination<SUDecrementDto>> GetSUDecrementsAsync(string sort, List<Filter> filters, int pageIndex,
        int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SUDecrements.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SUDecrement>(queryString, summaryColumns);

                return new Pagination<SUDecrementDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUDecrementDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SUDecrement>(queryString, summaryColumns);

                return new Pagination<SUDecrementDto>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SUDecrementDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUDecrementDto>)],
                    Total = total,
                };
        }
    }

    public async Task<List<SUDecrementAvailableSupply>> GetAvailableSupplyForDecrementAsync(DateTime refDate)
    {
        var availableSupplies = await context.Database
            .GetDbConnection()
            .QueryAsync<SUDecrementAvailableSupply>("GetAvailableSupplyForDecrement",
                new { BranchId = "", RefDate = refDate });

        return [.. availableSupplies];
    }

    public async Task<List<SupplyTransfer>> GetSupplyTransfersAsync()
    {
        var suIncrements = await context.SUIncrements.AsNoTracking()
            .ToListAsync();

        var supplyTransfers = suIncrements.Select(mapper.Map<SupplyTransfer>);

        return [.. supplyTransfers];
    }

    public async Task<Pagination<SUTransferDto>> GetSUTransfersAsync(string sort, List<Filter> filters, int pageIndex,
        int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SUTransfers.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SUTransfer>(queryString, summaryColumns);

                return new Pagination<SUTransferDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUTransferDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SUTransfer>(queryString, summaryColumns);

                return new Pagination<SUTransferDto>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SUTransferDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUTransferDto>)],
                    Total = total,
                };
        }
    }

    public async Task<SUTransferResponse> GetSUTransferDetailFullAsync(string id, int type)
    {
        var suTransfer = await context.SUTransfers
            .AsNoTracking()
            .Include(x => x.SUTransferDetails)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (suTransfer != null)
        {
            var response = new SUTransferResponse
            {
                SUTransfer = [mapper.Map<SUTransferDto>(suTransfer)],
                SUTransferDetail = [.. suTransfer.SUTransferDetails.Select(mapper.Map<SUTransferDetailDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<List<SUAllocationDto>> CheckExistsSUAllocationAsync(int month, int year)
    {
        var suAllocation = await context.SUAllocations.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Year == year && x.Month == month);

        if (suAllocation == null) return [];

        return [suAllocation.Adapt<SUAllocationDto>()];
    }

    public async Task<SUAllocationExpenseResponse> GetSUAllocationExpenseAsync(DateTime fromDate, DateTime toDate)
    {
        var multi = await context.Database
            .GetDbConnection()
            .QueryMultipleAsync("GetSUAllocationExpense", new { FromDate = fromDate, ToDate = toDate });

        var response = new SUAllocationExpenseResponse
        {
            ListExpenseData = [.. await multi.ReadAsync<SUAllocationDetailExpenseDto>()],
            ListDataAllocationTable = [.. await multi.ReadAsync<SUAllocationDetailTableDto>()],
            ListDataAllocationDetailPost = [.. await multi.ReadAsync<SUAllocationDetailPostDto>()],
        };

        return response;
    }

    public async Task<SUAllocationResponse> GetSUAllocationDetailFullAsync(string id, int type)
    {
        var suAllocation = await context.SUAllocations
            .AsNoTracking()
            .Include(x => x.SUAllocationDetailExpenses)
            .Include(x => x.SUAllocationDetailTables)
            .Include(x => x.SUAllocationDetailPosts)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (suAllocation != null)
        {
            var response = new SUAllocationResponse
            {
                SUAllocation = [mapper.Map<SUAllocationDto>(suAllocation)],
                SUAllocationDetailExpense =
                    [.. suAllocation.SUAllocationDetailExpenses.Select(mapper.Map<SUAllocationDetailExpenseDto>)],
                SUAllocationDetailTable =
                    [.. suAllocation.SUAllocationDetailTables.Select(mapper.Map<SUAllocationDetailTableDto>)],
                SUAllocationDetailPost =
                    [.. suAllocation.SUAllocationDetailPosts.Select(mapper.Map<SUAllocationDetailPostDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<Pagination<SUAdjustmentDto>> GetSUAdjustmentsAsync(string sort, List<Filter> filters,
        int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SUAdjustments.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SUAdjustment>(queryString, summaryColumns);

                return new Pagination<SUAdjustmentDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUAdjustmentDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SUAdjustment>(queryString, summaryColumns);

                return new Pagination<SUAdjustmentDto>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SUAdjustmentDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUAdjustmentDto>)],
                    Total = total,
                };
        }
    }

    public async Task<Pagination<SUAllocationDto>> GetSUAllocationsAsync(string sort, List<Filter> filters,
        int pageIndex, int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SUAllocations.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SUAllocation>(queryString, summaryColumns);

                return new Pagination<SUAllocationDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUAllocationDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SUAllocation>(queryString, summaryColumns);

                return new Pagination<SUAllocationDto>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SUAllocationDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUAllocationDto>)],
                    Total = total,
                };
        }
    }

    public async Task<SUAdjustmentResponse> GetSUAdjustmentDetailFullAsync(string id, int type)
    {
        var suAdjustment = await context.SUAdjustments
            .AsNoTracking()
            .Include(x => x.SUAdjustmentDetails)
            .Include(x => x.SUAdjustmentDetailVouchers)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (suAdjustment != null)
        {
            var response = new SUAdjustmentResponse
            {
                SUAdjustment = [mapper.Map<SUAdjustmentDto>(suAdjustment)],
                SUAdjustmentDetail = [.. suAdjustment.SUAdjustmentDetails.Select(mapper.Map<SUAdjustmentDetailDto>)],
                SUAdjustmentDetailVoucher =
                    [.. suAdjustment.SUAdjustmentDetailVouchers.Select(mapper.Map<SUAdjustmentDetailVoucherDto>)]
            };

            return response;
        }

        return null;
    }

    public async Task<List<SUAdjustmentSupplyAvailable>> GetSUAdjustmentSupplyAvailableAsync(DateTime refDate,
        Guid? refId)
    {
        var supplyAvailable = await context.Database
            .GetDbConnection()
            .QueryAsync<SUAdjustmentSupplyAvailable>("GetSUAdjustmentSupplyAvailable",
                new { RefDate = refDate, RefId = refId });

        return [.. supplyAvailable];
    }

    public async Task<List<string>> GetSUIncrementAllSupplyGroupAsync()
    {
        var supplyGroups = await context.SUIncrements.AsNoTracking()
            .Where(x => !string.IsNullOrWhiteSpace(x.SupplyGroup))
            .Select(x => x.SupplyGroup)
            .Distinct()
            .ToListAsync();

        return [.. supplyGroups];
    }

    public async Task<List<OrganizationUnitSupplyTransfer>> GetOrganizationUnitSupplyTransferAsync(Guid? supplyId,
        DateTime refDate)
    {
        var organizationUnits = await context.Database
            .GetDbConnection()
            .QueryAsync<OrganizationUnitSupplyTransfer>("GetOrganizationUnitSupplyTransfer",
                new { SupplyId = supplyId, RefDate = refDate });

        return [.. organizationUnits];
    }

    public async Task<List<RelatedVoucher>> GetSUArisingAsync(Guid refId, int refType, int displayOnBook)
    {
        var relatedVouchers = await context.Database
            .GetDbConnection()
            .QueryAsync<RelatedVoucher>("GetRelatedVoucher",
                new { RefId = refId, RefType = refType, DisplayOnBook = displayOnBook });

        return [.. relatedVouchers];
    }

    public async Task<List<SUAuditSupplyForAudit>> GetSupplyForAuditAsync(Guid? branchId, string auditDate,
        bool isManagementBook)
    {
        var supplyForAudit = await context.Database
            .GetDbConnection()
            .QueryAsync<SUAuditSupplyForAudit>("GetSupplyForAudit",
                new { BranchId = branchId, AuditDate = auditDate, IsManagementBook = isManagementBook });

        return [.. supplyForAudit];
    }

    public async Task<Pagination<SUAuditDto>> GetSUAuditsAsync(string sort, List<Filter> filters, int pageIndex,
        int pageSize, bool useSp, int view, List<int> summaryColumns, int loadMode)
    {
        var query = context.SUAudits.AsNoTracking();

        if (filters is { Count: > 0 })
            query = query.ApplyFilters(filters);

        var queryString = query.ToQueryString();

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var sorts = JsonConvert.DeserializeObject<List<Sort>>(sort);
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

                var summaryData = await context.GetSummaryAsync<SUAudit>(queryString, summaryColumns);

                return new Pagination<SUAuditDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUAuditDto>)],
                    SummaryData = summaryData,
                    Total = total,
                };
            case (int)EnumPagingDataType.Summary:
                summaryData = await context.GetSummaryAsync<SUAudit>(queryString, summaryColumns);

                return new Pagination<SUAuditDto>
                {
                    SummaryData = summaryData,
                    Total = total,
                };
            default:
                pageData = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new Pagination<SUAuditDto>
                {
                    PageData = [.. pageData.Select(mapper.Map<SUAuditDto>)],
                    Total = total,
                };
        }
    }

    public async Task<SUAuditResponse> GetSUAuditDetailFullAsync(string id, int type)
    {
        var suAudit = await context.SUAudits
            .AsNoTracking()
            .Include(x => x.SUAuditDetails)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.RefType == type);

        if (suAudit != null)
        {
            var response = new SUAuditResponse
            {
                SUAudit = [mapper.Map<SUAuditDto>(suAudit)],
                SUAuditDetail = [.. suAudit.SUAuditDetails.Select(mapper.Map<SUAuditDetailDto>)],
            };

            return response;
        }

        return null;
    }

    public async Task<List<GetDataOriginalResponse>> GetSUIncrementDataOriginalAsync(DateTime fromDate, DateTime toDate,
        int refType, List<Guid> listRefId, int displayOnBook, string accountList, Guid? supplyId)
    {
        var originals = await context.Database
            .GetDbConnection()
            .QueryAsync<GetDataOriginalResponse>("GetSUIncrementDataOriginal",
                new
                {
                    FromDate = fromDate, ToDate = toDate, RefType = refType, ListRefId = string.Join(",", listRefId),
                    AccountList = accountList, SupplyId = supplyId
                });

        return [.. originals];
    }

    public async Task<List<SUIncrementGetDataFromPUStock>> GetSUIncrementGetFromStockAsync(DateTime fromDate,
        DateTime toDate, int refType)
    {
        var stocks = await context.Database
            .GetDbConnection()
            .QueryAsync<SUIncrementGetDataFromPUStock>("GetSUIncrementGetFromStock",
                new { FromDate = fromDate, ToDate = toDate, RefType = refType });

        return [.. stocks];
    }
}