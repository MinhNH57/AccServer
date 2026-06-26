using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ref.API.Infrastructure;

namespace Ref.API.Apis;

public class RefServices(
    RefDbContext context,
    ILogger<RefServices> logger,
   [FromServices] IMediator mediator,
   [FromServices] IMapper mapper,
   [FromServices] ICurrentUser currentUser)
{
    public RefDbContext Context { get; } = context;
    public ILogger<RefServices> Logger { get; } = logger;
    public IMapper Mapper { get; } = mapper;
    public IMediator Mediator { get; } = mediator;
    public ICurrentUser CurrentUser => currentUser;
};
