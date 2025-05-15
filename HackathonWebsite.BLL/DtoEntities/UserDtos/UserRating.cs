using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWebsite.BLL.DtoEntities.UserDtos
{
    public class UserRating
    {
        public string Username { get; set; } = null!;
        public int Rating { get; set; }
    }
}
