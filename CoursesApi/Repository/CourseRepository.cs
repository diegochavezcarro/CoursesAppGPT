using CoursesApi.Models;
using System.Collections.Generic;

namespace CoursesApi.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private List<Course> _courses = new List<Course>
        {
            new Course { Id = 1, Name = "Course 1" },
            new Course { Id = 2, Name = "Course 2" }
        };

        public IEnumerable<Course> GetAll() => _courses;
        public Course? GetById(int id) => _courses.Find(c => c.Id == id);

        public Course Add(Course course)
        {
            course.Id = _courses.Max(c => c.Id) + 1;
            _courses.Add(course);
            return course;
        }

    }
}
