using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.WebAPI.Library.Models.Validations
{
    public class WarningValidation : Validation
    {
        public WarningValidation(string message)
        {
            this.Type = ValidationType.Error;
            this.Message = message;
        }
    }
}
