using AutoMapper.QueryableExtensions;
using SmartAccCloud.Application.Interfaces.Identities;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.Services.User;

public class UserService(IApplicationDbContext context, IMapper mapper, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<Result<List<UserVm>>> GetUserVm(CancellationToken token)
    {
        var lst = await context.Users.ProjectTo<UserVm>(mapper.ConfigurationProvider)
            .ToListAsync(token)
            .ConfigureAwait(false);

        return Result<List<UserVm>>.Success(lst);
    }

    public async Task<Result<UserVm>> GetByCode(string codeUser, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(codeUser);

        var user = await context.Users.Where(c => c.CodeUser == codeUser)
            .ProjectTo<UserVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: token)
            .ConfigureAwait(false);

        if (user is null)
            return Result<UserVm>.Failure(new Error("404", "Không tìm thấy user"));

        return Result<UserVm>.Success(user);
    }

    public async Task<Result<Guid>> CreateUser(CreateUpdateUserRequest request, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool isExistsCodeUser = await context.Users
            .AnyAsync(c => c.CodeUser == request.CodeUser.Trim(), cancellationToken: token)
            .ConfigureAwait(false);

        if (isExistsCodeUser)
            return Result<Guid>.Failure(new Error("400", "Mã user đã tồn tại"));

        var userCreate = new Users()
        {
            CodeUnit = request.CodeUnit,
            CodeUser = request.CodeUser,
            NameUser = request.NameUser,
            PassUser = !string.IsNullOrEmpty(request.Password)
                ? passwordHasher.EncryptMd5(request.Password)
                : passwordHasher.EncryptMd5("123"),
            Notes = request.Note
        };
        if (request.LstRules is not null)
        {
            foreach (var item in request.LstRules)
            {
                item.CodeUser = request.CodeUser;
                item.CodeUnit = request.CodeUnit ?? 100;
                item.Id = Guid.NewGuid();
            }

            var ruleCreate = mapper.Map<List<RuleUser>>(request.LstRules);
            context.RuleUser.AddRange(ruleCreate);
        }

        await context.Users.AddAsync(userCreate, token).ConfigureAwait(false);
        await context.SaveChangesAsync(token).ConfigureAwait(false);

        return Result<Guid>.Success(userCreate.Id);
    }

    public async Task<Result<bool>> UpdateUser(CreateUpdateUserRequest request, CancellationToken token)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(c => c.CodeUser == request.CodeUser, cancellationToken: token).ConfigureAwait(false);
        if (user is null)
            return Result<bool>.Failure(new Error("404", "Not found User"));

        mapper.Map(request, user);

        //if (string.IsNullOrWhiteSpace(request.PassUser))
        //{
        //    user.PassUser = !string.IsNullOrEmpty(request.Password)
        //        ? passwordHasher.EncryptMd5(request.Password)
        //        : passwordHasher.EncryptMd5("123");
        //}
        //else
        //{
        //    user.PassUser = !string.IsNullOrEmpty(request.PassUser)
        //        ? passwordHasher.EncryptMd5(request.PassUser)
        //        : passwordHasher.EncryptMd5("123");
        //}
        context.Users.Update(user);

        if (request.LstRules is not null)
        {
            var listRule = await context.RuleUser
                .Where(c => c.CodeUser == request.CodeUser)
                .ToListAsync(cancellationToken: token)
                .ConfigureAwait(false);

            foreach (var item in request.LstRules)
            {
                var existingRules = listRule.FirstOrDefault(c => c.KeyFuntion == item.KeyFuntion);
                if (existingRules is not null)
                    mapper.Map(item, existingRules);
            }

            context.RuleUser.UpdateRange(listRule);
        }

        await context.SaveChangesAsync(token).ConfigureAwait(false);

        return Result<bool>.Success(true);
    }
}