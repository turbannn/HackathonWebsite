using AutoMapper;
using HackathonWebsite.BLL.DtoEntities.UserDtos;
using HackathonWebsite.DAL.Entities;

namespace HackathonWebsite.BLL.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();

            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<UserReadDto, UserRating>();

            CreateMap<UserCreateDto, UserReadDto>();
        }
    }
}
