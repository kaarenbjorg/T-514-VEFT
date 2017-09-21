using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Entities
{
    /// <summary>
    /// This class represents a relationship between a course and a student, 
    /// students that are on a waiting list for the course.
    /// </summary>
    public class WaitingList
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
    }
}
