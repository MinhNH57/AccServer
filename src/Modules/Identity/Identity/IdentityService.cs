using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Identity.Services;
using MapsterMapper;
using MediatR;

namespace Identity;

public class IdentityService(
    IdentityDbContext dbContext,
    ILogger<IdentityService> logger,
  //  TenantInfoCustomize? tenant,
    [FromServices] SmartDataServices smartDataService,
    [FromServices] IPasswordHasher passwordHasher,
    [FromServices] ITokenService tokenService,
    [FromServices] ICurrentUser currentUser,
    [FromServices] IMapper mapper,
    [FromServices] IMediator mediator,
    [FromServices] IEmailService emailService,
    [FromServices] IMultiTenantContextAccessor tenantContextAccessor
    )
{
    //private readonly TenantInfoCustomize? tenant = tenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
    public IdentityDbContext DbContext { get; } = dbContext;
    public ILogger<IdentityService> Logger { get; } = logger;
    public IPasswordHasher PasswordHasher { get; } = passwordHasher;
    public ITokenService TokenService { get; } = tokenService;
    public IEmailService EmailService { get; } = emailService;
    public SmartDataServices SmartDataService { get; } = smartDataService;
    public ICurrentUser CurrentUser { get; } = currentUser;
    public IMapper Mapper { get; } = mapper;
    public IMediator Mediator => mediator;

    public IMultiTenantContextAccessor TenantContextAccessor { get; } = tenantContextAccessor;
   // public TenantInfoCustomize? Tenant => tenant;
}