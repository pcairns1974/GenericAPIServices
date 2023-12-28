using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.Data
{
    public class ApiRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiRequestException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }

}
