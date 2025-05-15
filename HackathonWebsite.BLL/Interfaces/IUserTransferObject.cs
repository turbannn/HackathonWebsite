using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWebsite.BLL.Interfaces
{
    public interface IUserTransferObject : ITransferObject
    {
        string Username { get; set; }
        string Role { get; set; }
        int Rating { get; set; }
    }
}
