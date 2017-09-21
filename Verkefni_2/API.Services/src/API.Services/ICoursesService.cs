using CourseAPI.Models.DTO;
using CourseAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Services
{
    public interface ICoursesService
    {
        List<CourseLiteDTO> GetCoursesBySemester(string semester);
        CourseDetailsDTO GetCourseByID(int id);
        List<StudentLiteDTO> GetStudentsInCourse(int id);
        List<StudentLiteDTO> GetStudentsOnWaitingList(int id);
        List<StudentLiteDTO> AddStudentToCourse(int id, StudentViewModel newStudent);
        CourseDetailsDTO UpdateCourse(int id, CourseViewModel updated);
        void DeleteCourse(int id);
        void DeleteStudentFromCourse(int id, string SSN);
        Tuple<int, CourseDetailsDTO> CreateCourse(CreateCourseViewModel newCourse);
        List<StudentLiteDTO> AddStudentToWaitingList(int id, StudentViewModel newStudent);
    }
}
