using BuildingBlocks.Dapper;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using Dapper;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using FirebaseAdmin.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notification.Data;
using Notification.Data.StoreProcedure;
using Notification.Service;
using System.Data;

namespace Notification.EventHandler;

public class ConfirmVoucherEventHandler(SmartDataServices smartData,
    IMultiTenantStore<TenantInfoCustomize> tenantStore,
    NotificationDbContext dbContext,
    IMultiTenantContextSetter multiTenantContextSetter) : IConsumer<ConfirmVoucherEvent>
{
    public async Task Consume(ConsumeContext<ConfirmVoucherEvent> context)
    {
        string tenantId = context.Headers.Get<string>(TenantConstant.TenantIdHeader)!;

        var tenantInfo = await tenantStore.TryGetByIdentifierAsync(tenantId);
        multiTenantContextSetter.MultiTenantContext = new MultiTenantContext<TenantInfoCustomize>
        { TenantInfo = tenantInfo };

        var getInfoSendNoficationStore = new GetInfoSendNofication(context.Message.Parameter, context.Message.IdVoucher, context.Message.CodeUser, context.Message.CodeUnit, moreInfo: context.Message.MoreInfo);
        var connection = dbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
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

                await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(multicastMessage);
                
            }
            else
            {
                var multicastMessage = new FireBaseMulticastBuilder([.. data.Select(c => c.TokenMessage)])
                    .WithAndroid()
                    .WithApns()
                    .WithNotification(data[0].TitleNoti, data[0].BodyNoti)
                    .Build();

                var result = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(multicastMessage);
            }
        }
    }
}