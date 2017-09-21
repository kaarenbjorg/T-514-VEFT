using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models.ViewModels
{
    /// <summary>
    /// This class represents a course created by a user
    /// </summary>
    public class CreateCourseViewModel
    {

        /// <summary>
        /// The template of the course
        /// Example: "T-111-PROG"
        /// </summary>
        [Required]
        public string TemplateID { get; set; }

        /// <summary>
        /// The start date of the course
        /// Example: 2016-09-03 08:30:4 4
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the course
        /// Example: 2016-13-19 15:40:00
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Semester of the course.
        /// Example: 20163 -> fall 2016
        /// </summary>
        [Required]
        public string Semester { get; set; }

        /// <summary>
        /// Number of students that can be enrolled in the class
        /// Example: 4
        /// </summary>
        [Required]
        public int MaxStudents { get; set; }
    }
}

