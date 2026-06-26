using BuildingBlocks.Dapper;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Data.StoreProcedure;
using Notification.Notification;

namespace Notification.EventHandler;

public class CreateQcEventHandler(IMediator mediator, SmartDataServices smartData,
    IMultiTenantStore<TenantInfoCustomize> tenantStore,
    NotificationDbContext dbContext,
    IMultiTenantContextSetter multiTenantContextSetter) : IConsumer<CreateDataQuanlityControlEvent>
{
    public async Task Consume(ConsumeContext<CreateDataQuanlityControlEvent> context)
    {
        string tenantId = context.Headers.Get<string>("X-Tenant-Id")!;
        string type = context.Headers.Get<string>("Type")!;

  
        var tenantInfo = await tenantStore.TryGetByIdentifierAsync(tenantId);
        multiTenantContextSetter.MultiTenantContext = new MultiTenantContext<TenantInfoCustomize>
        { TenantInfo = tenantInfo };

        //TODO:Map tạm TT gửi Noti cho đồng tháp sau phải sửa vì Code bẩn (T_T)
        if (type == "SalesAdmin")
        {
            var getInfoSendNoti1 = new GetInfoSendNofication("GetNofiConfig1", "",context.Message.UserCode, 888, "SalesAdmin");
            var data1 = await smartData.GetListObject<RecipientOfMessage>(getInfoSendNoti1.StoredProcedureName,
                dbContext.Database.GetConnectionString()!, getInfoSendNoti1.Parameters);

            
            if (data1.Count == 0) return;
            string title1 = "Có  phiếu cần duyệt";

            var notificationTopic1 = new NotificationMulticast()
            {
                Title = title1,
                Body = "Vừa có đơn hàng được tạo",
                Tokens = data1.Select(c => c.TokenMessage).ToList(),
                Data = new Dictionary<string, string>() { { "idVoucher", context.Message.IdVoucher.ToString() } }

            };
            var pushNotiTopic1 = new PushMulticastNotificationCommand(notificationTopic1);
            await mediator.Send(pushNotiTopic1);
            return;
        }



        var getInfoSendNoti = new GetInfoSendNofication("GetNofiConfig", context.Message.UserCode, "",888, context.Message.DataType ?? "Imp");
        var data = await smartData.GetListObject<RecipientOfMessage>(getInfoSendNoti.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, getInfoSendNoti.Parameters);

        if (data.Count == 0) return;
        string title = context.Message.IsREject
            ? $"Phiếu {context.Message.NumberOfVoucher} không đạt chất lượng"
            : $"Phiếu {context.Message.NumberOfVoucher} đạt chất lượng";

        var notificationTopic = new NotificationMulticast()
        {
            Title = title,
            Tokens = data.Select(c => c.TokenMessage).ToList(),
            Data = new Dictionary<string, string>() { { "idVoucher", context.Message.IdVoucher.ToString() } }
        };
        if (context.Message.IsREject)
            notificationTopic.Body = context.Message.Notes ?? "";

        var pushNotiTopic = new PushMulticastNotificationCommand(notificationTopic);
        await mediator.Send(pushNotiTopic);
    }
}