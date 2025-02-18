﻿using Microsoft.EntityFrameworkCore;
using registration.Entities;

namespace registration.Interfaces
{
    public interface IComfirmPasswordService
    {
        public bool ResetPasswordDb(string password, string email);
        public bool UpdateUser(User user);
    }
}
