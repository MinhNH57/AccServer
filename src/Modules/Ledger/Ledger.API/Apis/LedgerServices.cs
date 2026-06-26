using Ledger.API.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ledger.API.Apis;

public class LedgerServices(
    LedgerDbContext context,
    ILogger<LedgerServices> logger,
   [FromServices] IMediator mediator,
   [FromServices] IMapper mapper,
   [FromServices] ICurrentUser currentUser)
{
    public LedgerDbContext Context { get; } = context;
    public ILogger<LedgerServices> Logger { get; } = logger;
    public IMapper Mapper { get; } = mapper;
    public IMediator Mediator { get; } = mediator;
    public ICurrentUser CurrentUser => currentUser;
};
