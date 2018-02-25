using Nelibur.ObjectMapper;
using PTZ.Frw.DataAccess;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Interfaces;
using PTZ.Frw.WebAPI.Models;
using PTZ.Frw.WebAPI.Models.Users;
using System;
using System.Collections.Generic;

namespace PTZ.Frw.WebApi.Services.SignInManager
{
    public class SignInManager : ISignInManager
    {
        private readonly IUserRepository _usrMng;
        private readonly IUserService _userSvc;

        public SignInManager(IUserRepository usrMng, IUserService userSvc)
        {
            TinyMapper.Bind<RegisterRequest, User>();
            TinyMapper.Bind<RegisterRequest, UserDetails>();
            TinyMapper.Bind<User, UserDTO>();

            _usrMng = usrMng;
            _userSvc = userSvc;
        }

        public UserDTO IsValidLogin(AuthRequest authUserRequest, out List<Validation> validations)
        {
            validations = new List<Validation>();

            UserDTO userDto = null;
            User user = _usrMng.GetUserByUsername(authUserRequest.Username);

            if (user.Username != null &&
                String.Equals(user.Username, authUserRequest.Username, StringComparison.OrdinalIgnoreCase))
            {
                string answeredPwd = Utils.Crypto.PreparePassword(user.PasswordSalt, authUserRequest.Password);
                bool passwordMatch = answeredPwd == user.PasswordHash;

                if (!passwordMatch)
                {
                    validations.Add(new ErrorValidation("Could not verify username and password"));
                }

                userDto = TinyMapper.Map<UserDTO>(user);
            }
            else
            {
                validations.Add(new ErrorValidation("Could not verify username and password"));
            }

            return userDto;
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
