using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos
{
    public class TaskRatingReadDto : BaseDataTransferObject, ITaskTransferObject
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rating { get; set; }
        public string UserName { get; set; }
    }
}
