using Xunit;
using Moq;
using CoursesApi.Controllers;
using CoursesApi.Services;
using Microsoft.AspNetCore.Mvc;
using CoursesApi.Models;

namespace CoursesApi.Tests
{
    public class CoursesControllerTests
    {
        private readonly CoursesController _controller;
        private readonly Mock<ICourseService> _serviceMock;

        public CoursesControllerTests()
        {
            _serviceMock = new Mock<ICourseService>();
            _controller = new CoursesController(_serviceMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkObjectResult()
        {
            _serviceMock.Setup(service => service.GetAllCourses()).Returns(new List<Course>());
            var result = _controller.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ExistingId_ReturnsOkObjectResult(int id)
        {
            _serviceMock.Setup(service => service.GetCourseById(id)).Returns(new Course { Id = id });
            var result = _controller.GetById(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public void GetById_NonExistingId_ReturnsNotFoundResult(int id)
        {
            _serviceMock.Setup(service => service.GetCourseById(id)).Returns<Course>(null);
            var result = _controller.GetById(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateCourse_ValidCourse_ReturnsCreatedResponse()
        {
            // Arrange
            var newCourse = new Course { Name = "New Course" };
            _serviceMock.Setup(s => s.CreateCourse(It.IsAny<Course>())).Returns(newCourse);

            // Act
            var result = _controller.CreateCourse(newCourse);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Course>(actionResult.Value);
            Assert.Equal("New Course", returnValue.Name);
        }

        [Fact]
        public void CreateCourse_NullCourse_ReturnsBadRequest()
        {
            // Arrange
            Course newCourse = null;

            // Act
            var result = _controller.CreateCourse(newCourse);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
