using Nelibur.ObjectMapper;
using PTZ.Frw.DataAccess.Interfaces;
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
        private readonly IUserManager _usrMng;

        public SignInManager(IUserManager usrMng)
        {
            TinyMapper.Bind<RegisterRequest, User>();
            TinyMapper.Bind<RegisterRequest, UserDetails>();

            _usrMng = usrMng;
        }

        public bool CheckPasswordSignIn(User user, AuthRequest authUserRequest)
        {
            string answeredPwd = Utils.Crypto.PreparePassword(user.PasswordSalt, authUserRequest.Password);
            bool passwordMatch = answeredPwd == user.PasswordHash;

            return user.Username != null &&
                   String.Equals(user.Username, authUserRequest.Username, StringComparison.OrdinalIgnoreCase) &&
                   passwordMatch;
        }

        public bool RegisterUser(RegisterRequest registerRequest)
        {
            if (!_usrMng.UserExists(registerRequest.Username))
            {
                string guid = Guid.NewGuid().ToString();

                User newUser = TinyMapper.Map<User>(registerRequest);
                newUser.PasswordSalt = guid;
                newUser.PasswordHash = Utils.Crypto.PreparePassword(guid, registerRequest.Password);
                newUser.Details = TinyMapper.Map<UserDetails>(registerRequest);
                newUser = _usrMng.SaveUser(newUser);

                return newUser != null;
            }

            return false;
        }
    }
}
