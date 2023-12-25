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
    }
}
