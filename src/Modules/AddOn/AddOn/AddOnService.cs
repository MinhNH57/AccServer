using AddOn.Data;
using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AddOn;

public class AddOnService(
   [FromServices] SmartDataServices smartDataServices,
   AddOnDbContext dbContext,
   [FromServices]ICurrentUser currentUser,
   [FromServices]IConfiguration configuration,
   IMediator mediator,
    ILogger<AddOnService> logger)
{
    public SmartDataServices SmartDataServices => smartDataServices;
    public ILogger<AddOnService> Logger => logger;
    public AddOnDbContext DbContext => dbContext;
    public ICurrentUser CurrentUser => currentUser;
    public IConfiguration Configuration => configuration;
    public IMediator Mediator => mediator;
}