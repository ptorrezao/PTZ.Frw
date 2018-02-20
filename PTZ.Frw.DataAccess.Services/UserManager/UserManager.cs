using PTZ.Frw.DataAccess.Interfaces;
using PTZ.Frw.DataAccess.Models;
using System.Linq;

namespace PTZ.Frw.DataAccess.Services.UserManager
{
    public class UserManager : IUserManager
    {
        public User FindByEmail(string userName)
        {
            using (var context = new PTZFrwContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PTZFrwContext>() { }))
            {
                return context.Users.FirstOrDefault(x => x.UserName == userName);
            }
        }
    }
}
