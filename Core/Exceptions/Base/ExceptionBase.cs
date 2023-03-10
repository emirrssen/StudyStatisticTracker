using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions.Base
{
    public class ExceptionBase : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public ExceptionBase(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
