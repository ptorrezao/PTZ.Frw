using PTZ.Frw.DataAccess.Models;

namespace PTZ.Frw.DataAccess.Interfaces
{
    public interface IUserManager
    {
        User FindByEmail(string userName);
    }
}