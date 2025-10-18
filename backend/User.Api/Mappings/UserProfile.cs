using AutoMapper;
using User.Api.Dto;
using User.Api.Models;

namespace User.Api.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateUserDto, UserModel>()
            .ForAllMembers(opt
                => opt.Condition((_, _, srcMember)
                    => srcMember is not null));
    }
}