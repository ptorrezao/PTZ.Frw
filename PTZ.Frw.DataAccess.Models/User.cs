using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PTZ.Frw.DataAccess.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password{ get; set; }
    }
}
