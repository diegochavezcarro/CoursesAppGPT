using Xunit;
using CoursesApi.Repository;
using CoursesApi.Models;

namespace CoursesApi.Tests
{
    public class CourseRepositoryTests
    {
        private readonly CourseRepository _repository;

        public CourseRepositoryTests()
        {
            _repository = new CourseRepository();
        }

        [Fact]
        public void GetAll_ReturnsAllCourses()
        {
            var result = _repository.GetAll();
            Assert.True(result.Any());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ReturnsCorrectCourse(int id)
        {
            var result = _repository.GetById(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Add_AddsCourse()
        {
            var course = new Course
            {
                Id = 3,
                Name = "Course 3"
            };

            _repository.Add(course);

            var result = _repository.GetById(course.Id);
            Assert.NotNull(result);
            Assert.Equal(course.Id, result.Id);
            Assert.Equal(course.Name, result.Name);

        }

    }
}
