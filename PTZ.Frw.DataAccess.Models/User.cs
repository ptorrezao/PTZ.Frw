using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace PTZ.Frw.DataAccess.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("Id")]
        public virtual UserDetails Details { get; set; }
    }
}
