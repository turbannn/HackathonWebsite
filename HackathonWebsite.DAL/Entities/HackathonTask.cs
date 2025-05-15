
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonWebsite.DAL.Entities
{
    public class HackathonTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = null!;
        public int Rating { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
