using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebAPI.Library.Models.Validations
{
    public class Validation
    {
        public ValidationType Type { get; protected set; }
        public string Message { get; set; }
    }
}
