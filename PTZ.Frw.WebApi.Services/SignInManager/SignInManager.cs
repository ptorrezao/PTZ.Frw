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
            string answeredPwd = Utils.Crypto.PreparePassword(user.PasswordSalt, authUserRequest.Password);
            bool passwordMatch = answeredPwd == user.PasswordHash;

            return user.Username == authUserRequest.UserName && 
                   user.Username != null &&
                   passwordMatch;
        }
    }
}
