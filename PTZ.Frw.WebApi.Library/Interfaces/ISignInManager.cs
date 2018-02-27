using PTZ.Frw.WebAPI.Library.Models.Authentication;
using PTZ.Frw.WebAPI.Library.Models.Users;
using PTZ.Frw.WebAPI.Library.Models.Validations;
using System.Collections.Generic;

namespace PTZ.Frw.WebAPI.Library.Interfaces
{
    public interface ISignInManager
    {
        bool RegisterUser(RegisterRequest registerRequest);
        UserDTO IsValidLogin(AuthRequest authUserRequest, out List<Validation> validations);
    }
}
