using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos
{
    public class TaskProfileReadDto : BaseDataTransferObject, ITaskTransferObject
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rating { get; set; }

        public int UserId { get; set; }
    }
}
