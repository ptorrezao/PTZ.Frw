using PTZ.Frw.WebAPI.Library.Models.Users;
using System.Collections.Generic;

namespace PTZ.Frw.WebAPI.Library.Interfaces
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
