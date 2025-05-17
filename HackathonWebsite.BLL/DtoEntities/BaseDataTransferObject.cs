using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.DtoEntities
{
    public class BaseDataTransferObject : ITransferObject
    {
        public int Id { get; set; }
    }
}
