using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefNo.API.Infrastructure;

namespace RefNo.API.Apis;

public class RefNoServices(
    RefNoDbContext context,
    ILogger<RefNoServices> logger,
   [FromServices] IMediator mediator,
   [FromServices] IMapper mapper,
   [FromServices] ICurrentUser currentUser)
{
    public RefNoDbContext Context { get; } = context;
    public ILogger<RefNoServices> Logger { get; } = logger;
    public IMapper Mapper { get; } = mapper;
    public IMediator Mediator { get; } = mediator;
    public ICurrentUser CurrentUser => currentUser;
};
