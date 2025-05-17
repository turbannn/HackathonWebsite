namespace HackathonWebsite.BLL.Interfaces
{
    public interface ITaskTransferObject : ITransferObject
    {
        string Name { get; set; }
        string Description { get; set; }
        int Rating { get; set; }
    }
}
