using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos
{
    public class HackathonTaskDto : BaseDataTransferObject
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rating { get; set; }
    }
}
