using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.DtoEntities
{
    public class BaseDataTransferObject : ITransferObject
    {
        public int Id { get; set; }
    }
}
