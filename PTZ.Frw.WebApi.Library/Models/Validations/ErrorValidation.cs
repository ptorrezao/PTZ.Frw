using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebAPI.Library.Models.Validations
{
    public class ErrorValidation : Validation
    {
        public ErrorValidation(string message)
        {
            this.Type = ValidationType.Error;
            this.Message = message;
        }
    }
}
