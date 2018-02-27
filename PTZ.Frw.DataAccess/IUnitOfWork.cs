using PTZ.Frw.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        int Complete();
    }
}
