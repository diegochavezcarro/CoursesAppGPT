// ICourseService.cs
using CoursesApi.Models;

namespace CoursesApi.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course? GetCourseById(int id);
        Course CreateCourse(Course course);
    }
}
