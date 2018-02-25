using System.Collections.Generic;
using PTZ.Frw.DataAccess.Models;

namespace PTZ.Frw.DataAccess
{
    public interface IUserRepository
    {
        User GetUser(int key);

        User SaveUser(User user);

        bool UserExists(string username);

        List<User> GetUsers();

        User GetUserByUsername(string username);
    }
}