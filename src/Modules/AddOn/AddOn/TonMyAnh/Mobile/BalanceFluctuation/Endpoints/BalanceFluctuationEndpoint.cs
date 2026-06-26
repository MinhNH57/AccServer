using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using AddOn.Data.Entities;
using AddOn.TonMyAnh.Mobile.BalanceFluctuation.Models;
using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AddOn.TonMyAnh.Mobile.BalanceFluctuation.Endpoints;

public class BalanceFluctuationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Steel");

        var api = vApi.MapGroup("steels").HasApiVersion(1.0);

        //api.RequireAuthorization();

        api.MapPost("/create-balance-fluctuation", CreateBalanceFluctuation);
        api.MapPost("/create-balance-fluctuations", CreateBalanceFluctuations);

    }

    private async Task<IResult> CreateBalanceFluctuations(HttpRequest httpRequest,
        [Required][FromHeader(Name = "check-sum")] string checkSum,
        [AsParameters] AddOnService service,
        List<CreateBalanceFluctuationRequest> requests,
        CancellationToken token)
    {
        var isSuccess = await InsertData(service, requests, token);


        return Results.Ok(Result<bool>.Success(true));

        // return Results.BadRequest(Result<bool>.Failure(new Error("400", "Có lỗi khi insert")));
    }

    private async Task<IResult> CreateBalanceFluctuation(
        HttpRequest httpRequest,
        [Required][FromHeader(Name = "check-sum")] string checkSum,
        [AsParameters] AddOnService service,
        CreateBalanceFluctuationRequest request,
        CancellationToken token)
    {
        // httpRequest.Headers.TryGetValue("check-sum", out var checkSum);

        var jsonRequest = JsonConvert.SerializeObject(request);
        if (string.IsNullOrEmpty(checkSum) || !ValidateCheckSum(service.Configuration, checkSum, jsonRequest))
            return Results.BadRequest(Result.Failure(new Error("400", "Check sum không hợp lệ")));

        var isSuccess = await InsertData(service, [request], token);

        if (isSuccess)
            return Results.Ok(Result.Success(true));
        return Results.BadRequest(Result.Failure(new Error("400", "Có lỗi khi insert")));
    }

    private static bool ValidateCheckSum(IConfiguration configuration, string? checkSum, string value)
    {
        var secretKey = configuration.GetValue<string>("KeyHash")!;
        var input = $"{{{secretKey}}}+{{{value}}}";
        var hashString = ComputeSha256(input);

        return hashString == checkSum;
    }

    private static async Task<bool> InsertData(
        [AsParameters] AddOnService service,
        List<CreateBalanceFluctuationRequest> balanceFluctuations, CancellationToken token)
    {
        var listDistinct = balanceFluctuations.DistinctBy(c => c.Timestamp).ToList();
        foreach (var item in listDistinct)
        {
            var dataBalanceFluctuations = new DataBalanceFluctuations()
            {
                CodeUnit = item.CodeUnit,
                AmountBlance = Convert.ToDouble(item.AmountBlance),
                Amount = Convert.ToDouble(item.Amount),
                BankOfAccount = item.BankOfAccount,
                BankOfAccountReceive = item.BankOfAccountReceive,
                BankOfName = item.BankOfName,
                FullContent = item.FullContent,
                RemittanceContent = item.RemittanceContent,
                Notes = item.Notes,
                Timestamp = item.Timestamp,
                RecordDate = ToDateTime(item.Timestamp)
            };
            if (await service.DbContext.DataBalanceFluctuations.AnyAsync(c =>
                    c.Timestamp == dataBalanceFluctuations.Timestamp, cancellationToken: token))
            {
                continue;
            }
            await service.DbContext.DataBalanceFluctuations.AddAsync(dataBalanceFluctuations, token).ConfigureAwait(false);

        }

        return (await service.DbContext.SaveChangesAsync(token)) > 0;
    }

    /// <summary>
    /// Mã hoá sha256 + base64
    /// </summary>
    /// <param name="input">Chuỗi cần mã hóa</param>
    /// <returns></returns>
    private static string ComputeSha256(string input)
    {
        using SHA256 sha256 = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hashBytes);
    }
    public static DateTime ToDateTime(long timestamp)
    {
        var datatimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return datatimeOffset.ToOffset(TimeSpan.FromHours(7)).DateTime;
    }
}