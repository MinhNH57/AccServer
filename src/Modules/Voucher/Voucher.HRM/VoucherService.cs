using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Voucher.HRM.Infrastructure;

namespace Voucher.HRM;
public class VoucherService(VoucherHrmDbContext context,
    //IOptions<VoucherOptions> options,
    ILogger<VoucherService> logger,
   [FromServices] IMediator mediator,
    [FromServices] SmartDataServices smartDataServices,
   [FromServices] IMapper mapper,
   [FromServices] ICurrentUser currentUser)
{
    public VoucherHrmDbContext Context { get; } = context;
    // public IOptions<VoucherOptions> Options { get; } = options;
    public ILogger<VoucherService> Logger { get; } = logger;
    public IMapper Mapper { get; } = mapper;
    public SmartDataServices SmartDataServices { get; } = smartDataServices;
    public IMediator Mediator { get; } = mediator;
    public ICurrentUser CurrentUser => currentUser;
}
