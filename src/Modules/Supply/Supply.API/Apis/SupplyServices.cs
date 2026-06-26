public class SupplyServices(
    IMapper mapper,
    IMediator mediator,
    ISupplyQueries queries,
    IIdentityService identityService,
    ILogger<SupplyServices> logger)
{
    public IMapper Mapper { get; set; } = mapper;
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<SupplyServices> Logger { get; } = logger;
    public ISupplyQueries Queries { get; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}
