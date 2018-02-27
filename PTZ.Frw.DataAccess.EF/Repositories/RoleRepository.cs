using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTZ.Frw.DataAccess.EF.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(PTZFrwContext context)
            : base(context)
        {
        }

        public PTZFrwContext _context
        {
            get { return Context as PTZFrwContext; }
        }
    }
}
