using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using System.Data;
using BuildingBlocks.Web;

namespace Identity.User.GetUserInfo;

public class GetUserInfoStoreProduce : StoredProcedureBase
{
    private const string Parameter = "@Parameter";
    private const string UserNames = "@UserNames";
    private const string CodeUnit = "@CodeUnit";

    public GetUserInfoStoreProduce(string parameter, string userNames, int codeUnit = 100) : base("GetUserInfo")
    {
        Parameters.Add(Parameter, parameter, DbType.String);
        Parameters.Add(UserNames, userNames, DbType.String);
        Parameters.Add(CodeUnit, codeUnit, DbType.Int16);
    }
}

public record GetUserInfoQuery(string Parameter, string UserNames, int CodeUnit) : IQuery<Result>;

public class GetUserInfoQueryHandler(IdentityDbContext dbContext, SmartDataServices smartDataServices, ICurrentUser currentUser) : IQueryHandler<GetUserInfoQuery, Result>
{

    public async Task<Result> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var userInfoStore = new GetUserInfoStoreProduce(query.Parameter, currentUser.CodeUser!, currentUser.CodeUnit);

        var user = await smartDataServices.GetListObject<object>(userInfoStore.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, userInfoStore.Parameters, cancellationToken);
        return Result.Success(user);
     
        //if (query.Parameter == "CurrentUser")
        //{
        //    var userInfoStore = new GetUserInfoStoreProduce(query.Parameter, currentUser.CodeUser!, currentUser.CodeUnit);

        //    var user = await smartDataServices.GetListObject<object>(userInfoStore.StoredProcedureName,
        //        dbContext.Database.GetConnectionString()!, userInfoStore.Parameters, cancellationToken);
            
        //    return Result.Success(user);
        //}
        //else
        //{
        //    var userInfoStore = new GetUserInfoStoreProduce(query.Parameter, query.UserNames, query.CodeUnit);
        //    var user = await smartDataServices.GetListObject<Users>(userInfoStore.StoredProcedureName,
        //        dbContext.Database.GetConnectionString()!, userInfoStore.Parameters, cancellationToken);

        //    return Result.Success(user);
        //}
    }
}