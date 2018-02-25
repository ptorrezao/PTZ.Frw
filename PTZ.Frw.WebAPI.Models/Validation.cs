using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebAPI.Models
{
    public class Validation
    {
        public ValidationType Type { get; protected set; }
        public string Message { get; set; }
    }

    public class ErrorValidation : Validation
    {
        public ErrorValidation(string message)
        {
            this.Type = ValidationType.Error;
            this.Message = message;
        }
    }

    public class WarningValidation : Validation
    {
        public WarningValidation(string message)
        {
            this.Type = ValidationType.Warning;
            this.Message = message;
        }
    }
}
