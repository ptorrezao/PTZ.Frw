using PTZ.Frw.DataAccess.Interfaces;
using PTZ.Frw.DataAccess.Models;
using System.Linq;

namespace PTZ.Frw.DataAccess.Services.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly PTZFrwContext _context;

        public UserManager(PTZFrwContext ctx)
        {
            _context = ctx;
        }

        public User FindByEmail(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
