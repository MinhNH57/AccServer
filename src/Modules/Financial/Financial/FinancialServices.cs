using BuildingBlocks.Dapper;
using Financial.Infrastructure;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Financial;

public class FinancialServices(
    FinancialDbContext context,
    //IOptions<FinancialOptions> options,
    ILogger<FinancialServices> logger,
   [FromServices] IMediator mediator,
    [FromServices] SmartDataServices smartDataServices,
   [FromServices] IMapper mapper)
{
    public FinancialDbContext Context { get; } = context;
    // public IOptions<FinancialOptions> Options { get; } = options;
    public ILogger<FinancialServices> Logger { get; } = logger;
    public IMapper Mapper { get; } = mapper;
    public SmartDataServices SmartDataServices { get; } = smartDataServices;
    public IMediator Mediator { get; } = mediator;
};
