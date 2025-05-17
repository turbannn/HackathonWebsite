﻿namespace HackathonWebsite.BLL.Interfaces
{
    public interface IUserTransferObject : ITransferObject
    {
        string Username { get; set; }
        string Role { get; set; }
    }
}
