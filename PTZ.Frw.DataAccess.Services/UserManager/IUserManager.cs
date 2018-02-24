using PTZ.Frw.DataAccess.Models;

namespace PTZ.Frw.DataAccess.Interfaces
{
    public interface IUserManager
    {
        User FindByUsername(string userName);

        User SaveUser(User user);
    }
}