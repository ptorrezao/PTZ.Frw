using PTZ.Frw.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
    }
}
