using CoursesApi.Models;
using CoursesApi.Repository;

namespace CoursesApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService(ICourseRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Course> GetAllCourses() => _repository.GetAll();
        public Course GetCourseById(int id) => _repository.GetById(id);
    }
}
