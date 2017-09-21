using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Entities
{
    /// <summary>
    /// This class represents a course stored in database.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Unique ID for the course
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The template of the course
        /// Example: "T-111-PROG"
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// Semester of the course.
        /// Example: 20163 -> fall 2016
        /// </summary>
        public string Semester { get; set;  }

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
        /// The date when course was stored in database
        /// Example: 2016-13-19 15:40:00
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Number of students that can be enrolled in the class
        /// Example: 4
        /// </summary>
        public int MaxStudents { get; set; }
    }
}
