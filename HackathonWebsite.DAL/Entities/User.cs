﻿namespace HackathonWebsite.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public List<HackathonTask> Tasks { get; set; } = null!;
    }
}
