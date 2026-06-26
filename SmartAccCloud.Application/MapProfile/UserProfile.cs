using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Application.MapProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Users, UserVm>();
        CreateMap<CreateUpdateUserRequest, Users>();
    }
}

public class RulesProfile : Profile
{
    public RulesProfile()
    {
        CreateMap<RuleUser, RuleUserVm>().ReverseMap();
    }
}