using PTZ.Frw.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PTZ.Frw.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly PTZFrwContext _context;

        public UserRepository(PTZFrwContext ctx)
        {
            _context = ctx;
        }

        public User GetUser(int key)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == key);

            if (user != null)
            {
                PerformValidations(user);
            }

            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = _context.Users.FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                PerformValidations(user);
            }

            return user;
        }

        public List<User> GetUsers()
        {
            List<User> users = _context.Users.ToList();
            return users;
        }

        public User SaveUser(User user)
        {
            if (user.Id > 0)
            {
                _context.Users.Update(user);
            }
            else
            {
                _context.Users.Add(user);

            }
            _context.SaveChanges();

            return _context.Users.FirstOrDefault(x => x.Username == user.Username);
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(x => x.Username == username);
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
