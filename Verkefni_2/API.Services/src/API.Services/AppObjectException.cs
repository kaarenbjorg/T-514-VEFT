using System;

namespace CourseAPI.Services
{
    public class AppObjectNotFoundException : Exception
    {
        public AppObjectNotFoundException()
        {
        }

        public AppObjectNotFoundException(string message) : base(message)
        {
        }

        public AppObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}