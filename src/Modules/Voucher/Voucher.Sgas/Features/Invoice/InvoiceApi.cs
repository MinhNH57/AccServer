using BuildingBlocks.Response;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Voucher.Sgas.Features.Invoice.CreateInvoiceFromMultiplePumpCode;
using Voucher.Sgas.Features.Invoice.CreateInvoiceFromPumpCode;
using Voucher.Sgas.Features.Invoice.RoundUpMoney;
using Voucher.Sgas.Model.Invoice;

namespace Voucher.Sgas.Features.Invoice;

public class InvoiceApi : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Voucher");
        var api = vApi.MapGroup("voucher/invoice/").HasApiVersion(1.0).RequireAuthorization();

        api.MapPost("create-invoice-from-pump-code", CreateInvoice)
            .WithSummary("Tạo mới hóa đơn từ mã bơm.")
            .WithDescription("Tạo mới hóa đơn từ mã bơm.")
            .WithTags("SgasVoucher");

        api.MapPost("publish-invoice", PublishInvoicesAsync)
            .WithSummary("Phát hành hóa đơn.")
            .WithDescription("Phát hành hóa đơn.")
            .WithTags("SgasVoucher");

        api.MapPost("create-invoice-from-multiple-pump-code", CreateInvoiceFromMultiplePumpCodesAsync)
            .WithSummary("Tạo hóa đơn SMART từ nhiều mã bơm.")
            .WithDescription("Tạo hóa đơn SMART từ nhiều mã bơm.")
            .WithTags("SgasVoucher");

        api.MapPost("round-up-money-smart", RoundUpMoney)
            .WithSummary("Cập nhật số tiền làm tròn vào hóa đơn SMART.")
            .WithDescription("Cập nhật số tiền làm tròn vào hóa đơn SMART.")
            .WithTags("SgasVoucher");
    }
    private async Task<IResult> CreateInvoice([AsParameters] VoucherServices services, CreateInvoiceRequest request, CancellationToken token)
    {
        var query = new CreateInvoiceFromPumpCodeCommand(request);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }

    private async Task<IResult> PublishInvoicesAsync([AsParameters] VoucherServices services, PublishInvoicesRequest request, CancellationToken token)
    {

        var jsonContent = JsonConvert.SerializeObject(new
        {
            unit_code = services.CurrentUser.CodeUnit.ToString(),
            warehouse_code = services.CurrentUser.WarehoseCode,
            user_code = services.CurrentUser.CodeUser,
            ids = request.Ids,
            provider_name = request.ProviderName
        });

        var result = await TransferCommandAndProcessResult(services, HttpMethod.Post, "publish-invoices", jsonContent);
        if (!string.IsNullOrEmpty(result))
        {
            var res = JsonConvert.DeserializeObject<dynamic>(result);
            if (res != null && res.status.code == 200)
            {
                return Results.Ok(Result.Success(res.data.ToString()));
            }
        } 
        return Results.BadRequest(Result.Failure(new Error(ErrorCode.Forbidden, "Xảy ra lỗi khi phát hành hóa đơn")));
    }
    private async Task<IResult> RoundUpMoney([AsParameters] VoucherServices services, RoundUpMoneyRequest request, CancellationToken token)
    {
        var query = new RoundUpMoneyCommand(request);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }
    private async Task<IResult> CreateInvoiceFromMultiplePumpCodesAsync([AsParameters] VoucherServices services, CreateInvoiceFromMultiplePumpCodesRequest request, CancellationToken token)
    {
        var query = new CreateInvoiceFromMultiplePumpCodeCommand(request);
        var result = await services.Mediator.Send(query, token);
        return Results.Ok(result);
    }
    #region CALL SMARTSOFTWARE API

    private async Task<string> GetTokenAsync()
    {
        var client = new HttpClient()
        {
            BaseAddress = new Uri("https://id.ssoftware.vn")
            //BaseAddress = new Uri("https://localhost:5001")
        };

        var requestBody = new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("scope", "invoice.read invoice.write"),
            new KeyValuePair<string, string>("client_id", "7332a54658df49fdb8265964e537a4a4"),
            new KeyValuePair<string, string>("client_secret", "4f904de76a9645ce9aa8ef9c2165542e")
        };

        var content = new FormUrlEncodedContent(requestBody);

        // Make the HTTP POST request
        var response = await client.PostAsync("/connect/token", content);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return result.access_token;
        }

        return null;
    }

    private async Task<string> TransferCommandAndProcessResult(VoucherServices services, HttpMethod httpMethod, string requestUrl, string content)
    {
        var token = await GetTokenAsync();
        if (token != null)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.ssoftware.vn/api/v1/invoice/");
            //client.BaseAddress = new Uri("https://apitest.ssoftware.vn/api/v1/invoice/");
            //client.BaseAddress = new Uri("https://localhost:7223/api/v1/invoice/");

            // Add the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Add custom headers to the request
            client.DefaultRequestHeaders.Add("X-Tenant-Id", services.CurrentUser.StationId);
            client.DefaultRequestHeaders.Add("X-Request-Id", Guid.NewGuid().ToString());

            // Make the HTTP POST request
            if (requestUrl.StartsWith("/")) requestUrl = requestUrl.Substring(1);

            var request = new HttpRequestMessage(httpMethod, requestUrl);

            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
                if (content != null)
                {
                    // Convert the request content to JSON format
                    var requestContent = new StringContent(content, Encoding.UTF8, "application/json");
                    request.Content = requestContent;
                }

            // Send the request asynchronously using SendAsync method
            var response = await client.SendAsync(request);
                var teest = await response.Content.ReadAsStringAsync();
             
            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        return string.Empty;
    }

    #endregion
}