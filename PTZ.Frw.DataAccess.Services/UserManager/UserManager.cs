using PTZ.Frw.DataAccess.Interfaces;
using PTZ.Frw.DataAccess.Models;
using System;
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

        public User FindByUsername(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == userName);

            PerformValidations(user);

            return user;
        }

        public User SaveUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return _context.Users.FirstOrDefault(x => x.Username == user.Username);
        }

        private void PerformValidations(User user)
        {
            bool userHasChanges = false;

            //Password Validation
            if (user.PasswordSalt == null)
            {
                user.PasswordSalt = Guid.NewGuid().ToString();
                user.PasswordHash = Utils.Crypto.PreparePassword(user.PasswordSalt, user.PasswordHash);

                userHasChanges = true;
            }

            if (userHasChanges)
            {
                this.SaveUser(user);
            }
        }
    }
}
