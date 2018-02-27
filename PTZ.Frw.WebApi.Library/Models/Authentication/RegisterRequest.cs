using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebAPI.Library.Models.Authentication
{
    public class RegisterRequest : AuthRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
