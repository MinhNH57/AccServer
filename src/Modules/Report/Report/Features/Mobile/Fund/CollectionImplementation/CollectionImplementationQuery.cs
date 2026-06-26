using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Response;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Report.Infrastructure;
using Report.Infrastructure.StoredProcedures;
using Report.Infrastructure.Views;
using Report.Models;

namespace Report.Features.Mobile.Fund.CollectionImplementation;

public record CollectionImplementationQuery(
    string Parameter,
    string? Code,
    string? BeginDate,
    string? EndDate,
    string? Type,
    string? FundSourceCode) : IQuery<Result>;

public class FundSmartLatePaymentSummary
{
    public Guid Id { get; set; }
    //public string? CodeManager { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Gốc
    /// </summary>
    public decimal BalancePrincipalDue { get; set; }
    /// <summary>
    /// Lãi
    /// </summary>
    public decimal BalanceInterestDue { get; set; }
    /// <summary>
    /// Gốc + lãi
    /// </summary>
    public decimal BalancePrincipalInterestDue { get; set; }


    public decimal ContractValue { get; set; }

    /// <summary>
    /// Hoa hồng  CĐCS
    /// </summary>
    public decimal? RiskyDebtII { get; set; } = 0;
    /// <summary>
    /// Hoa hồng  CĐCT
    /// </summary>
    public decimal? RiskyDebtIII { get; set; } = 0;
    /// <summary>
    /// Hoa hồng  CTV
    /// </summary>
    public decimal? RiskyDebtIV { get; set; } = 0;
    /// <summary>
    /// Tổng thực thu
    /// </summary>
    public decimal? TotalPrincipalCollected { get; set; } = 0;
    /// <summary>
    ///  TKBB thu
    /// </summary>
    public decimal? BalanceSavings { get; set; }
    /// <summary>
    /// TKBB hoàn trả
    /// </summary>
    public decimal? RiskyDebtOver { get; set; } = 0;
    public float? PercentDue { get; set; } //Kỳ trả nợ

}

public class CollectionImplementationQueryHandler(ReportDbContext dbContext, ICurrentUser currentUser, SmartDataServices dataServices) : IQueryHandler<CollectionImplementationQuery, Result>
{
    public async Task<Result> Handle(CollectionImplementationQuery query, CancellationToken cancellationToken)
    {
        var values = new { Parameter = "RuleBaseUnionView", MaUser = currentUser.CodeUser, CodeUnit = 888 };
        var lstDataRule = await dataServices
            .GetListObject<RuleBaseUnion>("CreateRuleFucnAndReport", dbContext.Database.GetConnectionString()!, values, cancellationToken)
            .ConfigureAwait(false) ?? new();
        List<string?> listVillageCode = lstDataRule
            .Where(c => !string.IsNullOrEmpty(c.CodeObject)).Select(c => c.CodeObject)
            .ToList();

        var store = new FundSmartLatePaymentStore(query.Parameter, query.Code, currentUser.CodeUser!,
            888, "", query.BeginDate, query.EndDate, "", "", "", "", "", query.FundSourceCode);

        var data = await dataServices.
            GetListObject<ReportFundSmartLatePayment>(store.StoredProcedureName, dbContext.Database.GetConnectionString()!, store.Parameters, cancellationToken);

        var dataSummary = SummaryValue(data, query.Type, query.Code, listVillageCode);
        return Result.Success(dataSummary.ToList());
    }

    private List<FundSmartLatePaymentSummary> SummaryValue(in List<ReportFundSmartLatePayment> data, string condition, string code, List<string> listVillageCode)
    {
        List<FundSmartLatePaymentSummary> dataSummary;
        List<ReportFundSmartLatePayment> dataFilter;
        switch (condition)
        {
            //Lấy theo công đoàn cấp trên đang ql
            case "wand":
                dataSummary = (from item in data
                               group item by new { item.CodeWards, item.NameWards, item.PercentDue }
                   into g
                               select new FundSmartLatePaymentSummary
                               {
                                   Id = Guid.NewGuid(),
                                   Code = g.Key.CodeWards,
                                   Name = g.Key.NameWards,
                                   PercentDue = g.Key.PercentDue,
                                   BalanceInterestDue = g.Sum(c => c.BalanceInterestDue) ?? 0,
                                   BalancePrincipalDue = g.Sum(c => c.BalancePrincipalDue) ?? 0,
                                   BalancePrincipalInterestDue = g.Sum(c => c.BalancePrincipalInterestDue) ?? 0,
                                   //ContractValue = g.Sum(c => c.ContractValue) ?? 0,
                                   RiskyDebtIII = g.Sum(c => c.RiskyDebtIII) ?? 0,
                                   RiskyDebtIV = g.Sum(c => c.RiskyDebtIV) ?? 0,
                                   BalanceSavings = g.Sum(c => c.BalanceSavings),
                                   RiskyDebtOver = g.Sum(c => c.RiskyDebtOver),
                                   TotalPrincipalCollected = g.Sum(c => c.TotalPrincipalCollected),
                               }).ToList();

                break;
            //Lấy theo công đoàn cơ sở
            case "group":
                var str = code.Split(":");
                if (str.Length != 2) return new();
                //var test = data.Where(c => c.CodeWards == str[1] && c.CodeManager == str[0]).ToList();
                dataSummary = (from item in data.Where(c => c.CodeWards == str[1])
                               group item by new { item.GroupCode, item.GroupName, item.PercentDue }
                   into g
                               select new FundSmartLatePaymentSummary
                               {
                                   Id = Guid.NewGuid(),
                                   Code = g.Key.GroupCode,
                                   Name = g.Key.GroupName,
                                   PercentDue = g.Key.PercentDue,
                                   BalanceInterestDue = g.Sum(c => c.BalanceInterestDue) ?? 0,
                                   BalancePrincipalDue = g.Sum(c => c.BalancePrincipalDue) ?? 0,
                                   BalancePrincipalInterestDue = g.Sum(c => c.BalancePrincipalInterestDue) ?? 0,
                                   //ContractValue = g.Sum(c => c.ContractValue) ?? 0,
                                   RiskyDebtII = g.Sum(c => c.RiskyDebtII) ?? 0,
                                   RiskyDebtIV = g.Sum(c => c.RiskyDebtIV) ?? 0,
                                   BalanceSavings = g.Sum(c => c.BalanceSavings),
                                   RiskyDebtOver = g.Sum(c => c.RiskyDebtOver),
                                   TotalPrincipalCollected = g.Sum(c => c.TotalPrincipalCollected),
                               }).ToList();
                break;
            case "detail":
                dataSummary = (from item in data.Where(c => c.GroupCode == code)
                               group item by new { item.ObjectCode, item.ObjectName, item.PercentDue }
                    into g
                               select new FundSmartLatePaymentSummary
                               {
                                   Id = Guid.NewGuid(),
                                   Code = g.Key.ObjectCode,
                                   Name = g.Key.ObjectName,
                                   PercentDue = g.Key.PercentDue,
                                   BalanceInterestDue = g.Sum(c => c.BalanceInterestDue) ?? 0,
                                   BalancePrincipalDue = g.Sum(c => c.BalancePrincipalDue) ?? 0,
                                   BalancePrincipalInterestDue = g.Sum(c => c.BalancePrincipalInterestDue) ?? 0,
                                   //ContractValue = g.Sum(c => c.ContractValue) ?? 0,
                                   BalanceSavings = g.Sum(c => c.BalanceSavings),
                                   RiskyDebtIV = g.Sum(c => c.RiskyDebtIV) ?? 0,
                                   RiskyDebtII = g.Sum(c => c.RiskyDebtII) ?? 0,
                                   RiskyDebtOver = g.Sum(c => c.RiskyDebtOver),
                                   TotalPrincipalCollected = g.Sum(c => c.BalancePrincipalInterestDue + c.BalanceSavings - c.RiskyDebtOver),
                               }).ToList();
                break;
            //Theo nhóm người quản lý từng CDCT
            default:
                //dataFilter = currentUser.CodeUser == "ADMIN" ? data : data.Where(c => c.CodeManager == currentUser.CodeUser).ToList();
                //dataSummary = (from item in dataFilter
                //               group item by new { item.CodeManager, item.NameManager }
                //    into g
                //               select new FundSmartLatePaymentSummary
                //               {
                //                   Id = Guid.NewGuid(),
                //                   Code = string.IsNullOrWhiteSpace(g.Key.CodeManager) ? "" : g.Key.CodeManager,
                //                   Name = string.IsNullOrWhiteSpace(g.Key.NameManager) ? "Không có mã" : g.Key.NameManager,
                //                   BalanceInterestDue = g.Sum(c => c.BalanceInterestDue) ?? 0,
                //                   BalancePrincipalDue = g.Sum(c => c.BalancePrincipalDue) ?? 0,
                //                   BalancePrincipalInterestDue = g.Sum(c => c.BalancePrincipalInterestDue) ?? 0,
                //                   ContractValue = g.Sum(c => c.ContractValue) ?? 0
                //               }).ToList();
                //break;
                dataSummary = (from item in data
                               group item by new { item.CodeWards, item.NameWards, item.PercentDue }
                    into g
                               select new FundSmartLatePaymentSummary
                               {
                                   Id = Guid.NewGuid(),
                                   Code = g.Key.CodeWards,
                                   PercentDue = g.Key.PercentDue,
                                   Name = g.Key.NameWards,
                                   BalanceInterestDue = g.Sum(c => c.BalanceInterestDue) ?? 0,
                                   BalancePrincipalDue = g.Sum(c => c.BalancePrincipalDue) ?? 0,
                                   BalancePrincipalInterestDue = g.Sum(c => c.BalancePrincipalInterestDue) ?? 0,
                                   RiskyDebtIII = g.Sum(c => c.RiskyDebtIII) ?? 0,
                                   BalanceSavings = g.Sum(c => c.BalanceSavings),
                                   RiskyDebtOver = g.Sum(c => c.RiskyDebtOver),
                                   TotalPrincipalCollected = g.Sum(c => c.TotalPrincipalCollected),
                               }).ToList();
                break;
        }

        return dataSummary;
    }
}