using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;
using HackathonWebsite.DAL.Entities;
using AutoMapper;

namespace HackathonWebsite.BLL.MapProfiles
{
    public class HackathonTaskProfile : Profile
    {
        public HackathonTaskProfile()
        {
            CreateMap<HackathonTask, HackathonTaskDto>();

            CreateMap<HackathonTaskDto, HackathonTask>();
        }
    }
}
