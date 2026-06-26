using System.Data;
using BuildingBlocks.Messaging.Events;
using Carter;
using Dapper;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notification.Data.StoreProcedure;
using Notification.Models;
using Notification.Service;

namespace Notification.Notification;

public class Apis : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Notication");

        var api = vApi.MapGroup("notications/").HasApiVersion(1.0);
        //api.RequireAuthorization();

        api.MapPost("push-notification-multicast", PushNotificationMultiCast)
            .WithSummary("Gửi thông báo tới nhiều thiết bị qua Token message")
            .WithTags("Notication");

        api.MapPost("push-notification-topic", PushNotificationTopic)
            .WithSummary("Gửi thông báo tới nhiều thiết bị qua Topic")
            .WithTags("Notication");


        api.MapPost("get-info-send-noti", GetInfoSendNoti)
            .WithSummary("Láy thông tin để gửi thông báo")
            .WithTags("Notication");

        //api.MapPost("send-notification", SendNotfication);
    }

    private async Task<IResult> GetInfoSendNoti([AsParameters] NotificationService service,
        ConfirmVoucherEvent request, CancellationToken token)
    {
        var getInfoSendNoficationStore = new GetInfoSendNofication(request.Parameter, request.IdVoucher, request.CodeUser, request.CodeUnit, moreInfo: request.MoreInfo);
        var connection = service.DbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(token);
        }

        var multipleResult = await connection.QueryMultipleAsync(getInfoSendNoficationStore.StoredProcedureName, getInfoSendNoficationStore.Parameters).ConfigureAwait(false);
        var data = (await multipleResult.ReadAsync<RecipientOfMessage>()).ToList();
        if (data.Count > 0)
        {
            if (data[0].IsData && !multipleResult.IsConsumed)
            {
                var dataNoti = await multipleResult.ReadSingleAsync<object>().ConfigureAwait(false);
                var dataJson = JsonConvert.SerializeObject(dataNoti);
                var dictParam = new Dictionary<string, string>() { { "Type", "ConfirmVoucherScreen" }, { "Data", dataJson } };
                var multicastMessage = new FireBaseMulticastBuilder([.. data.Select(c => c.TokenMessage)])
                    .WithAndroid()
                    .WithApns()
                    .WithNotification(data[0].TitleNoti, data[0].BodyNoti)
                    .WithData(dictParam)
                    .Build();

                await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(multicastMessage, token);

            }
            else
            {
                var multicastMessage = new FireBaseMulticastBuilder([.. data.Select(c => c.TokenMessage)])
                    .WithAndroid()
                    .WithApns()
                    .WithNotification(data[0].TitleNoti, data[0].BodyNoti)
                    .Build();

                var result = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(multicastMessage, token);
            }
        }
        return TypedResults.NoContent();
    }


    private async Task<NoContent> PushNotificationMultiCast([AsParameters] NotificationService service,
        NotificationMulticast request, CancellationToken token)
    {
        var query = new PushMulticastNotificationCommand(request);

        var result = await service.Mediator.Send(query, token);
        return TypedResults.NoContent();
    }

    private async Task<IResult> PushNotificationTopic([AsParameters] NotificationService service,
        NotificationTopic request, CancellationToken token)
    {
        var query = new PushNotificationTopicCommand(request);

        var result = await service.Mediator.Send(query, token);
        return Results.NoContent();
    }
}