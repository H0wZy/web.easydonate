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

        CreateMap<UserModel, UserDto>()
            .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => Convert.ToBase64String(src.HashPassword)))
            .ForMember(dest => dest.SaltPassword, opt => opt.MapFrom(src => Convert.ToBase64String(src.SaltPassword)))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (int)src.UserType));
    }
}