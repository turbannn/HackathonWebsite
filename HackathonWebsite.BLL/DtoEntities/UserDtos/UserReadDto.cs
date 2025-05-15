using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;
using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.DtoEntities.UserDtos
{
    public class UserReadDto : BaseDataTransferObject, IUserTransferObject
    {
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;

        public List<TaskProfileReadDto> Tasks { get; set; } = null!;
    }
}
