using System.Net;

namespace Engine.Exceptions
{
    public abstract class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
