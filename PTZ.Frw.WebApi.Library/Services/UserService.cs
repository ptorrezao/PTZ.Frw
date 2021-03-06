﻿using Nelibur.ObjectMapper;
using PTZ.Frw.DataAccess;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Library.Interfaces;
using PTZ.Frw.WebAPI.Library.Models.Users;
using System;
using System.Collections.Generic;

namespace PTZ.Frw.WebAPI.Library.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            TinyMapper.Bind<User, UserDTO>(c =>
            {
                c.Bind(s => s.Role.Name, t => t.Role);
            });

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
            User user = _unitOfWork.Users.GetWithRole(key);

            return TinyMapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> users = _unitOfWork.Users.GetAllWithRole();

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
