using CourseAPI.Entities;
using CourseAPI.Models.DTO;
using CourseAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly AppDataContext _db;
        private readonly string DefaultSemester = "20163";

        public CoursesService(AppDataContext db)
        {
            _db = db;
        }
        public List<CourseLiteDTO> GetCoursesBySemester(string semester)
        {
            if (semester == null)
            {
                semester = DefaultSemester;
            }


            var result = (from c in _db.Courses
                          where c.Semester == semester
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          select new CourseLiteDTO
                          {
                              TemplateID = ct.TemplateID,
                              Semester = c.Semester,
                              NumberOfStudents = (from cs in _db.CourseStudent
                                          where cs.CourseID == c.ID
                                          join s in _db.Students on cs.SSN equals s.SSN
                                          select s).Count()
                          }).ToList();
            return result;
        }
        public CourseDetailsDTO GetCourseByID(int id)
        {
            var result = (from c in _db.Courses
                          where c.ID == id
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          select new CourseDetailsDTO
                          {
                              TemplateID = ct.TemplateID,
                              Name = ct.Name,
                              Semester = c.Semester,
                              StartDate = c.StartDate,
                              EndDate = c.EndDate,
                              Students = (from cs in _db.CourseStudent
                                          where cs.CourseID == id
                                          join s in _db.Students on cs.SSN equals s.SSN
                                          select new StudentLiteDTO
                                          {
                                              SSN = s.SSN,
                                              Name = s.Name
                                          }).ToList()
                          }).SingleOrDefault();
            if (result == null)
            {
                throw new AppObjectNotFoundException();
            }
            return result;
        }

        public Tuple<int, CourseDetailsDTO> CreateCourse(CreateCourseViewModel newCourse)
        {
            var course = _db.CourseTemplates.SingleOrDefault(x => x.TemplateID == newCourse.TemplateID);
            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var addCourse = new Course { TemplateID = newCourse.TemplateID, StartDate = newCourse.StartDate, EndDate = newCourse.EndDate, Semester = newCourse.Semester, MaxStudents = newCourse.MaxStudents, DateCreated = DateTime.Now };
            _db.Courses.Add(addCourse);
            _db.SaveChanges();

            var id = addCourse.ID;

            var result = GetCourseByID(id);

            return Tuple.Create(id, result);
        }

        public List<StudentLiteDTO> GetStudentsInCourse(int id)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);

            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var result = (from cs in _db.CourseStudent
                       join s in _db.Students on cs.SSN equals s.SSN
                       where cs.CourseID == id && cs.Active == 1 
                       select new StudentLiteDTO
                       {
                           SSN = s.SSN,
                           Name = s.Name
                       }).ToList();
            return result;
        }

        public List<StudentLiteDTO> GetStudentsOnWaitingList(int id)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);

            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var result = (from cs in _db.WaitingList
                          join s in _db.Students on cs.SSN equals s.SSN
                          where cs.CourseID == id
                          select new StudentLiteDTO
                          {
                              SSN = s.SSN,
                              Name = s.Name
                          }).ToList();
            return result;
        }

        public List<StudentLiteDTO> AddStudentToCourse(int id, StudentViewModel newStudent)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);
            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var NumberOfStudents = (from cs in _db.CourseStudent
                                    where cs.CourseID == course.ID && cs.Active == 1
                                    join s in _db.Students on cs.SSN equals s.SSN
                                    select s).Count();

            if (NumberOfStudents >= course.MaxStudents)
            {
                throw new AppDataPreconditionFailedException();
            }
            var student = _db.Students.SingleOrDefault(x => x.SSN == newStudent.SSN);
            if(student == null)
            {
                throw new AppObjectNotFoundException();
            }

            var studentInCourse = _db.CourseStudent.SingleOrDefault(x => x.SSN == newStudent.SSN && x.CourseID == id);
            if(studentInCourse != null && studentInCourse.Active == 1)
            {
                throw new AppDataPreconditionFailedException();
            }

            var studentInWaitingList = _db.WaitingList.SingleOrDefault(x => x.SSN == newStudent.SSN && x.CourseID == id);
            if (studentInWaitingList != null)
            {
                _db.WaitingList.Remove(studentInWaitingList);
            }
            var addStudent = new CourseStudent { SSN = newStudent.SSN, CourseID = id, Active = 1 };
            _db.CourseStudent.Add(addStudent);
            _db.SaveChanges();


            var result = (from cs in _db.CourseStudent
                          join s in _db.Students on cs.SSN equals s.SSN
                          where cs.CourseID == id
                          select new StudentLiteDTO
                          {
                              SSN = s.SSN,
                              Name = s.Name
                          }).ToList();
            return result;
        }

        public List<StudentLiteDTO> AddStudentToWaitingList(int id, StudentViewModel newStudent)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);
            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var student = _db.Students.SingleOrDefault(x => x.SSN == newStudent.SSN);
            if (student == null)
            {
                throw new AppObjectNotFoundException();
            }

            var studentInCourse = _db.CourseStudent.SingleOrDefault(x => x.SSN == newStudent.SSN && x.CourseID == id);
            if (studentInCourse != null && studentInCourse.Active == 1)
            {
                throw new AppDataPreconditionFailedException();
            }

            var studentInWaitingList = _db.WaitingList.SingleOrDefault(x => x.SSN == newStudent.SSN && x.CourseID == id);
            if (studentInWaitingList != null)
            {
                throw new AppDataPreconditionFailedException();
            }
            var addStudent = new WaitingList { SSN = newStudent.SSN, CourseID = id};
            _db.WaitingList.Add(addStudent);
            _db.SaveChanges();


            var result = (from cs in _db.WaitingList
                          join s in _db.Students on cs.SSN equals s.SSN
                          where cs.CourseID == id
                          select new StudentLiteDTO
                          {
                              SSN = s.SSN,
                              Name = s.Name
                          }).ToList();
            return result;
        }

        public void DeleteStudentFromCourse(int id, string SSN)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);

            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var student = (from cs in _db.CourseStudent
                           where cs.CourseID == id
                           && cs.SSN == SSN
                           select cs).SingleOrDefault();

            if (student.Active != 1)
            {
                throw new AppObjectNotFoundException();
            }
            student.Active = 0;
            _db.SaveChanges();

        }


        public CourseDetailsDTO UpdateCourse(int id, CourseViewModel updated)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);
            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            if(updated.StartDate != DateTime.MinValue)
            {
                course.StartDate = updated.StartDate;
            }

            if(updated.EndDate != DateTime.MinValue)
            {
                course.EndDate = updated.EndDate;
            }

            _db.SaveChanges();

            var result = (from c in _db.Courses
                          where c.ID == id
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          select new CourseDetailsDTO

                          {
                              TemplateID = ct.TemplateID,
                              Name = ct.Name,
                              Semester = c.Semester,
                              StartDate = c.StartDate,
                              EndDate = c.EndDate,
                              Students = (from cs in _db.CourseStudent
                                          where cs.CourseID == id
                                          join s in _db.Students on cs.SSN equals s.SSN
                                          select new StudentLiteDTO
                                          {
                                              SSN = s.SSN,
                                              Name = s.Name
                                          }).ToList()
                          }).SingleOrDefault();
            return result;
        }

        public void DeleteCourse(int id)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);

            if (course == null)
            {
                throw new AppObjectNotFoundException();
            }

            var courseWStudents = (from cs in _db.CourseStudent
                                   where cs.CourseID == id
                                   select cs).ToList();

            _db.Courses.Remove(course);

            foreach (var courseW in courseWStudents)
            {
                _db.CourseStudent.Remove(courseW);
            }
            _db.SaveChanges();
        }
    }
}
