using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PTZ.Frw.WebAPI.Models.Users
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
