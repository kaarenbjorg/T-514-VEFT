using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Services
{
    public class AppDataPreconditionFailedException : Exception
    {
        public AppDataPreconditionFailedException()
        {
        }

        public AppDataPreconditionFailedException(string message) : base(message)
        {
        }

        public AppDataPreconditionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
