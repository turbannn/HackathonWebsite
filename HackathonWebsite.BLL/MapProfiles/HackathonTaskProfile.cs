using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;
using HackathonWebsite.DAL.Entities;
using AutoMapper;

namespace HackathonWebsite.BLL.MapProfiles
{
    public class HackathonTaskProfile : Profile
    {
        public HackathonTaskProfile()
        {
            CreateMap<HackathonTask, TaskProfileReadDto>();
            CreateMap<HackathonTask, TaskRatingReadDto>();

            CreateMap<TaskCreateDto, HackathonTask>();
            CreateMap<TaskUpdateDto, HackathonTask>();

            CreateMap<HackathonTask, TaskRatingReadDto>().ForMember(dest => dest.UserName,
                opt =>
                {
                    opt.MapFrom(src => src.User.Username);
                });
        }
    }
}
