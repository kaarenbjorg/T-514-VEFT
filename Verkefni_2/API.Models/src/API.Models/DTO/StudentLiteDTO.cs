using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models.DTO
{
    /// <summary>
    /// This class represents a student.
    /// </summary>
    public class StudentLiteDTO
    {
        /// <summary>
        /// The social security number of the student
        /// Example: "1234567890"
        /// </summary>
        public string SSN { get; set; }

        /// <summary>
        /// The name of the student
        /// Example: "Jón Jónsson"
        /// </summary>
        public string Name { get; set; }
    }
}
