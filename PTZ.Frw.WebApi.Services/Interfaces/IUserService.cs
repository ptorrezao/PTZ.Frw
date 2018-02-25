using System;
using System.Collections.Generic;
using System.Text;
using PTZ.Frw.WebAPI.Models.Users;

namespace PTZ.Frw.WebAPI.Interfaces
{
    public interface IUserService
    {
        UserDTO FindByUsername(string username);
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUser(int key);
        UserDTO SaveUser(UserDTO value);
        void DeleteUser(int id);
    }
}
