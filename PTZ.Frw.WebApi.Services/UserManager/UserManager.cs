using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebApi.Services.UserManager
{
    public class UserManager : IUserManager
    {
        public User FindByEmail(string userName)
        {
            return new User() { UserName = userName, Id = 1 };
        }
    }
}
