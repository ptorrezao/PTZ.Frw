using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Interfaces;
using PTZ.Frw.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebApi.Services.SignInManager
{
    public class SignInManager : ISignInManager
    {
        public bool CheckPasswordSignIn(User user, AuthRequest authUserRequest)
        {
            return user.UserName == authUserRequest.UserName && user.UserName != null;
        }
    }
}
