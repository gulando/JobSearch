using System;
using System.Net;

namespace JobSearch.ApplicationCore.Common.Error
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public override string Message { get; }

        public CustomException(HttpStatusCode status, string message = default)
        {
            StatusCode = status;
            Message = message;
        }
    }
}