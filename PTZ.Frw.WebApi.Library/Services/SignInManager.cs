using Nelibur.ObjectMapper;
using PTZ.Frw.DataAccess;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Library.Interfaces;
using PTZ.Frw.WebAPI.Library.Models.Authentication;
using PTZ.Frw.WebAPI.Library.Models.Users;
using PTZ.Frw.WebAPI.Library.Models.Validations;
using System;
using System.Collections.Generic;

namespace PTZ.Frw.WebAPI.Library.Services
{
    public class SignInManager : ISignInManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public SignInManager(IUnitOfWork unitOfWork)
        {
            TinyMapper.Bind<RegisterRequest, User>();
            TinyMapper.Bind<RegisterRequest, UserDetails>();

            _unitOfWork = unitOfWork;
        }

        public UserDTO IsValidLogin(AuthRequest authUserRequest, out List<Validation> validations)
        {
            validations = new List<Validation>();

            UserDTO userDto = null;
            User user = _unitOfWork.Users.GetByUsername(authUserRequest.Username);

            if (user != null &&
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
            if (!_unitOfWork.Users.Any(x => x.Username == registerRequest.Username))
            {
                string guid = Guid.NewGuid().ToString();

                User newUser = TinyMapper.Map<User>(registerRequest);
                newUser.PasswordSalt = guid;
                newUser.PasswordHash = Utils.Crypto.PreparePassword(guid, registerRequest.Password);
                newUser.Details = TinyMapper.Map<UserDetails>(registerRequest);

                newUser.Role = _unitOfWork.Roles.SingleOrDefault(x => x.DefaultRole);
                _unitOfWork.Users.Add(newUser);

                return _unitOfWork.Complete() > 0;
            }

            return false;
        }
    }
}
