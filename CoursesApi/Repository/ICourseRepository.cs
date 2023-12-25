// ICourseRepository.cs
using CoursesApi.Models;
using System.Collections.Generic;

namespace CoursesApi.Repository
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course? GetById(int id);

        Course Add(Course course);
    }
}
