using System;
using System.Collections.Generic;
using System.Text;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Models;

namespace PTZ.Frw.WebAPI.Interfaces
{
    public interface ISignInManager
    {
        bool CheckPasswordSignIn(User user, AuthRequest authUserRequest);
        bool RegisterUser(RegisterRequest registerRequest);
    }
}
