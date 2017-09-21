using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Entities
{
    /// <summary>
    /// This class represents a course name stored in database.
    /// </summary>
    public class CourseTemplate
    {
        /// <summary>
        /// The name of the course
        /// Example: "Forritun"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The template of the course
        /// Example: "T-111-PROG"
        /// </summary>
        [Key]
        public string TemplateID { get; set; }

    }
}
