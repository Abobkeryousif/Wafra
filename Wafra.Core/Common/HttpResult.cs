using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Common
{
    public class HttpResult<T> where T : class
    {
        public HttpResult(HttpStatusCode statusCode, string message)
        {
            StatusCode = (int)statusCode;
            Message = message;
        }

        public HttpResult(HttpStatusCode statusCode, string message, T data) 
        {
            StatusCode = (int)statusCode;
            Message = message;
            Data = data;
        }
        public int StatusCode {  get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
