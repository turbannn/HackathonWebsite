﻿
namespace HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos
{
    public class TaskRatingDto : BaseDataTransferObject
    {
        public int Rating { get; set; }
        public int TeacherIdRatedBy { get; set; }

    }
}
