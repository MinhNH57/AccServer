using BuildingBlocks.CQRS;
using BuildingBlocks.Dapper;
using BuildingBlocks.Web;
using Serilog;

namespace Identity.User.ResetUserInfo;

public class TypeReset
{
    public bool Password { get; set; }
    public bool IdDevice { get; set; }
    public bool Rollcal { get; set; }
    public bool DeleteRollcal { get; set; }
    public string CodeUser { get; set; } = string.Empty;
}

public record ResetUserInfoCommand(TypeReset TypeReset) : ICommand<Result>;


public class ResetUserInfoCommandHandler(SmartDataServices smartDataServices, IdentityDbContext dbContext, ICurrentUser currentUser) : ICommandHandler<ResetUserInfoCommand, Result>
{
    public async Task<Result> Handle(ResetUserInfoCommand command, CancellationToken cancellationToken)
    {
        var store = new ResetUserInfoStore(command.TypeReset.Password, command.TypeReset.IdDevice,
            command.TypeReset.Rollcal, command.TypeReset.DeleteRollcal, command.TypeReset.CodeUser);
        var result = await smartDataServices.ExcuteNonQueryAsync(store.StoredProcedureName,
            dbContext.Database.GetConnectionString()!, store.Parameters);

        string typeReset = string.Empty;
        if (command.TypeReset.Password)
            typeReset = "PASSWORD";
        else if (command.TypeReset.IdDevice)
            typeReset = "ID_DEVICE";
        else if (command.TypeReset.Rollcal)
            typeReset = "RESET_ROLL_CALL";
        else if (command.TypeReset.DeleteRollcal)
            typeReset = "DELETE_ROLL_CALL";
        Log.Information("[RESET_INFO] User: {0} - {1} - for user: {2}", currentUser.CodeUser, typeReset, command.TypeReset.CodeUser);

        return Result.Success("ok");
    }

}
