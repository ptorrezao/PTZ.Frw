using Microsoft.EntityFrameworkCore;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTZ.Frw.DataAccess.EF.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PTZFrwContext context)
            : base(context)
        {
        }

        public PTZFrwContext _context
        {
            get { return Context as PTZFrwContext; }
        }

        public IEnumerable<User> GetAllWithRole()
        {
            return _context
                    .Users
                    .Include(x => x.Role)
                    .ToList();
        }

        public User GetByUsername(string username)
        {
            return _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefault(x => x.Username == username);
        }

        public User GetWithRole(int id)
        {
            return _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefault(x => x.Id == id);
        }

        public override void Save(User entity)
        {
            bool userExists = _context.Users.Any(x => x.Id == entity.Id) && entity.Id > 0;

            if (userExists)
            {
                User existingUser = _context.Users.First(x => x.Id == entity.Id);
                existingUser.FirstName = entity.FirstName;
                existingUser.MiddleName = entity.MiddleName;
                existingUser.LastName = entity.LastName;

                _context.Users.Update(existingUser);
            }
            else
            {
                entity.Id = 0;

                entity.Details = entity.Details ?? new UserDetails();

                _context.Users.Add(entity);
            }
        }
    }
}
