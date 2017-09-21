using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models.ViewModels
{
    /// <summary>
    /// This class represents a Social Security number of a given student.
    /// </summary>
    public class StudentViewModel
    {
        /// <summary>
        /// The Social Security number. Note that the lenght of the 
        /// string is ten characters, because SSN that are less or 
        /// more are not a valid SSN
        /// </summary>
        [Required, StringLength(10, MinimumLength = 10)]
        public string SSN { get; set; }
    }
}
