using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Entities
{
    /// <summary>
    /// This class represents a relationship between a course and a student, 
    /// students that are registered in each course.
    /// </summary>
    public class CourseStudent
    {
        ///<summary>
        /// A database generated id.
        /// </summary>
        [Key]
        public int ID { get; set; }

        ///<summary>
        /// A reference to the single course
        /// </summary>
        public int CourseID { get; set; }

        ///<summary>
        /// A reference to the single student
        /// </summary>
        public string SSN { get; set; }

        ///<summary>
        /// 1 if student is in class, 0 if student has been removed from class
        /// </summary>
        public int Active { get; set; }
    }
}
