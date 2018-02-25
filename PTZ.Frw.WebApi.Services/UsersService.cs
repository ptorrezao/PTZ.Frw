﻿using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;
using PTZ.Frw.DataAccess;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Interfaces;
using PTZ.Frw.WebAPI.Models.Users;

namespace PTZ.Frw.WebApi.Services.UserManager
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usrMng;

        public UserService(IUserRepository usrMng)
        {
            TinyMapper.Bind<User, UserDTO>();
            TinyMapper.Bind<List<User>, List<UserDTO>>();

            _usrMng = usrMng;
        }

        public UserDTO FindByUsername(string username)
        {
            User user = _usrMng.GetUserByUsername(username);

            return TinyMapper.Map<UserDTO>(user);
        }

        public UserDTO GetUser(int key)
        {
            User user = _usrMng.GetUser(key);

            return TinyMapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            List<User> users = _usrMng.GetUsers();
            return TinyMapper.Map<List<UserDTO>>(users);
        }
    }
}