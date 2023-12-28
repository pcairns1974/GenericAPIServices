using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.Data
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Content { get; private set; }

        public ApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, HttpStatusCode statusCode, string content)
            : base(message)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public ApiException(string message, HttpStatusCode statusCode, string content, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
