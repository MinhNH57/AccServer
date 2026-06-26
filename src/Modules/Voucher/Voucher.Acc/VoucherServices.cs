using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Voucher.Acc.Infrastructure;

namespace Voucher.Acc;

public class VoucherServices(
    VoucherDbContext context,
    //IOptions<VoucherOptions> options,
    ILogger<VoucherServices> logger,
   [FromServices] IMediator mediator,
    [FromServices] SmartDataServices smartDataServices,
   [FromServices] IMapper mapper,
   [FromServices] ICurrentUser currentUser)
{
    public VoucherDbContext Context { get; } = context;
    // public IOptions<VoucherOptions> Options { get; } = options;
    public ILogger<VoucherServices> Logger { get; } = logger;
    public IMapper Mapper { get; } = mapper;
    public SmartDataServices SmartDataServices { get; } = smartDataServices;
    public IMediator Mediator { get; } = mediator;
    public ICurrentUser CurrentUser => currentUser;
};
