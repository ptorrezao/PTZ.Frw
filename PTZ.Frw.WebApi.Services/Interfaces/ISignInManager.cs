using System;
using System.Collections.Generic;
using System.Text;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Models;
using PTZ.Frw.WebAPI.Models.Users;

namespace PTZ.Frw.WebAPI.Interfaces
{
    public interface ISignInManager
    {
        bool RegisterUser(RegisterRequest registerRequest);
        UserDTO IsValidLogin(AuthRequest authUserRequest, out List<Validation> validations);
    }
}
