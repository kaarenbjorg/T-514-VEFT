using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Entities;

namespace CourseAPI.Models.DTO
{
    /// <summary>
    /// This class represents a detailed information about a given course.
    /// </summary>
    public class CourseDetailsDTO
    {
        /// <summary>
        /// The template of the course
        /// Example: "T-111-PROG"
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// The name of the course
        /// Example: "Forritun"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Semester of the course.
        /// Example: 20163 -> fall 2016
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// The start date of the course
        /// Example: 2016-09-03 08:30:44
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the course
        /// Example: 2016-13-19 15:40:00
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// List of students in the course
        /// </summary>
        public List<StudentLiteDTO> Students { get; set; }
    }
}
