using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Salary.Infrastructure;

namespace Salary
{
    public class SalaryServices(
        SalaryDbContext context,
        //IOptions<SalaryOptions> options,
        ILogger<SalaryServices> logger,
        [FromServices] IMediator mediator,
        [FromServices] SmartDataServices smartDataServices,
        [FromServices] IMapper mapper,
        [FromServices] ICurrentUser currentUser)
    {
        public SalaryDbContext Context { get; } = context; 
        public ILogger<SalaryServices> Logger { get; } = logger;
        public IMapper Mapper { get; } = mapper;
        public SmartDataServices SmartDataServices { get; } = smartDataServices;
        public IMediator Mediator { get; } = mediator;
        public ICurrentUser CurrentUser => currentUser;
    };

}
