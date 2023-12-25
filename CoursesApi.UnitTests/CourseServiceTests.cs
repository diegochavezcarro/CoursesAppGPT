using Xunit;
using Moq;
using CoursesApi.Services;
using CoursesApi.Repository;
using CoursesApi.Models;
using System.Collections.Generic;

namespace CoursesApi.Tests
{
    public class CourseServiceTests
    {
        private readonly CourseService _service;
        private readonly Mock<ICourseRepository> _repositoryMock;

        public CourseServiceTests()
        {
            _repositoryMock = new Mock<ICourseRepository>();
            _service = new CourseService(_repositoryMock.Object);
        }

        [Fact]
        public void GetAllCourses_ReturnsAllCourses()
        {
            _repositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Course> { new Course(), new Course() });
            var result = _service.GetAllCourses();
            Assert.Equal(2, result.Count());
        }

        [Theory]
        [InlineData(1)]
        public void GetCourseById_ReturnsCourse(int id)
        {
            _repositoryMock.Setup(repo => repo.GetById(id)).Returns(new Course { Id = id });
            var result = _service.GetCourseById(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}
