using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseAPI.Entities
{
    /// <summary>
    /// This class represents a student stored in database.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// The social security number of the student, also a unique id for the student
        /// Example: "1234567890"
        /// </summary>
        [Key]
        public string SSN { get; set; }

        /// <summary>
        /// The name of the student
        /// Example: "Jón Jónsson"
        /// </summary>
        public string Name { get; set; }
    }
}
