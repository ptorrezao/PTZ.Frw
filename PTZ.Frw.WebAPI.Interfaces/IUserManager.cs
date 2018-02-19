using PTZ.Frw.DataAccess.Models;

namespace PTZ.Frw.WebAPI.Interfaces
{
    public interface IUserManager
    {
        User FindByEmail(string userName);
    }
}