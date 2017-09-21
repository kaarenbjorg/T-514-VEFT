using System;

namespace CourseAPI.Services
{
    public class AppObjectBadRequestException : Exception
    {
        public AppObjectBadRequestException()
        {
        }

        public AppObjectBadRequestException(string message) : base(message)
        {
        }

        public AppObjectBadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}