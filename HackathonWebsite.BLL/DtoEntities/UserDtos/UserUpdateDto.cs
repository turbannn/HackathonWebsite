using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.DtoEntities.UserDtos
{
    public class UserUpdateDto : BaseDataTransferObject, IUserTransferObject
    {
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
