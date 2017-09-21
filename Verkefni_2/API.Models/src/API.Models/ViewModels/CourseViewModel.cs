using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// This class represents a starting date and ending date in a given course.
/// </summary>
namespace CourseAPI.Models.ViewModels
{
    public class CourseViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
