using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;
using PTZ.Frw.DataAccess;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.DataAccess.Repositories;
using PTZ.Frw.WebAPI.Interfaces;
using PTZ.Frw.WebAPI.Models.Users;

namespace PTZ.Frw.WebApi.Services.UserManager
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            TinyMapper.Bind<User, UserDTO>();
            TinyMapper.Bind<UserDTO, User>();
            TinyMapper.Bind<List<User>, List<UserDTO>>();

            _unitOfWork = unitOfWork;
        }

        public void DeleteUser(int id)
        {
            User user = _unitOfWork.Users.Get(id);
            
            _unitOfWork.Users.Remove(user);

            _unitOfWork.Complete();
        }

        public UserDTO FindByUsername(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);

            return TinyMapper.Map<UserDTO>(user);
        }

        public UserDTO GetUser(int key)
        {
            User user = _unitOfWork.Users.Get(key);

            return TinyMapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> users = _unitOfWork.Users.GetAll();

            return TinyMapper.Map<List<UserDTO>>(users);
        }

        public UserDTO SaveUser(UserDTO userDto)
        {
            User user = TinyMapper.Map<User>(userDto);

            _unitOfWork.Users.Save(user);

            this.PerformValidations(user);

            _unitOfWork.Complete();

            return TinyMapper.Map<UserDTO>(user);
        }

        private void PerformValidations(User user)
        {
            //Password Validation
            if (user.PasswordSalt == null)
            {
                user.PasswordSalt = Guid.NewGuid().ToString();
                user.PasswordHash = Utils.Crypto.PreparePassword(user.PasswordSalt, user.PasswordHash);
            }
        }
    }
}
