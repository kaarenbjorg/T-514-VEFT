using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models.DTO
{
    /// <summary>
    /// This class represents a general information of a given course.
    /// </summary>
    public class CourseLiteDTO
    {
        /// <summary>
        /// The template of the course
        /// Example: "T-111-PROG"
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// Semester of the course.
        /// Example: 20163 -> fall 2016
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// Number of students in the course
        /// Example: 3
        /// </summary>
        public int NumberOfStudents { get; set; }

    }
}
