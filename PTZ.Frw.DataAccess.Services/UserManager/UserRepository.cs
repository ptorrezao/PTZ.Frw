using Microsoft.EntityFrameworkCore;
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

        public void DeleteUser(int id)
        {
            User user = _context.Users.Include(x => x.Details).FirstOrDefault(x => x.Id == id);

            _context.Entry(user.Details).State = EntityState.Deleted;

            _context.Users.Remove(user);
            _context.SaveChanges();
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
            bool userExists = _context.Users.Any(x => x.Id == user.Id) && user.Id > 0;

            if (userExists)
            {
                _context.Users.Update(user);
            }
            else
            {
                user.Id = 0;

                user.Details = user.Details ?? new UserDetails();

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
