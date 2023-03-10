using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions.Base;

namespace Core.Exceptions
{
    public class ValidatorException : ExceptionBase
    {
        public ValidatorException(string message) : base(message, HttpStatusCode.BadRequest) { }
    }
}
