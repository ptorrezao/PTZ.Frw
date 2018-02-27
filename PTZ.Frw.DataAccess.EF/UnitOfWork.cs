using System;
using System.Collections.Generic;
using System.Text;
using PTZ.Frw.DataAccess.EF.Repositories;
using PTZ.Frw.DataAccess.Repositories;

namespace PTZ.Frw.DataAccess.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private static bool isInitalizated;
        private readonly PTZFrwContext _context;

        public UnitOfWork(PTZFrwContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);

            PerformSeed();
        }

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private void PerformSeed()
        {
            if (!isInitalizated)
            {
                AddRoles();
                AddUser();

                isInitalizated = true;
            }
        }

        private void AddUser()
        {
            if (Users.SingleOrDefault(x => x.Username == "Administrator") == null)
            {
                string passwordSalt = Guid.NewGuid().ToString();
                Users.Add(new Models.User()
                {
                    FirstName = "Administrator",
                    Username = "Administrator",
                    Role = Roles.SingleOrDefault(x => x.Name == "Administrator"),
                    Details = new Models.UserDetails()
                    {

                    },
                    PasswordSalt = passwordSalt,
                    PasswordHash = Utils.Crypto.PreparePassword(passwordSalt, "admin")
                });
                this.Complete();
            }
        }

        private void AddRoles()
        {
            string defaultRole = "User";
            string[] defaultRoles = new string[] { defaultRole, "Administrator" };

            foreach (var role in defaultRoles)
            {
                if (!Roles.Any(x => x.Name == role))
                {
                    Roles.Add(new Models.Role()
                    {
                        Name = role,
                        DefaultRole = defaultRole == role
                    });
                }
            }
            this.Complete();
        }
    }
}
