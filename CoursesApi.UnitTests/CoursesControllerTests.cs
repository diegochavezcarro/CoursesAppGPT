using Xunit;
using Moq;
using MediatR;
using CoursesApi.Controllers;
using CoursesApi.Models;
using CoursesApi.Queries;
using CoursesApi.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class CoursesControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly CoursesController _controller;

    public CoursesControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new CoursesController(_mockMediator.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllCourses()
    {
        // Arrange
        var courses = new List<Course>
        {
            new Course { Id = 1, Name = "Course 1" },
            new Course { Id = 2, Name = "Course 2" }
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetAllCoursesQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(courses);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCourses = Assert.IsAssignableFrom<IEnumerable<Course>>(okResult.Value);
        Assert.Equal(2, (returnedCourses as List<Course>).Count);
    }

    [Fact]
    public async Task GetById_ReturnsCourse()
    {
        // Arrange
        var course = new Course { Id = 1, Name = "Course 1" };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetCourseByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(course);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCourse = Assert.IsType<Course>(okResult.Value);
        Assert.Equal(course.Id, returnedCourse.Id);
        Assert.Equal(course.Name, returnedCourse.Name);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundForInvalidId()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<GetCourseByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Course)null);

        // Act
        var result = await _controller.GetById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateCourse_ReturnsCreatedCourse()
    {
        // Arrange
        var course = new Course { Name = "New Course" };
        _mockMediator.Setup(m => m.Send(It.IsAny<CreateCourseCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Course { Id = 1, Name = "New Course" });

        // Act
        var result = await _controller.CreateCourse(course);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedCourse = Assert.IsType<Course>(createdAtActionResult.Value);
        Assert.Equal("New Course", returnedCourse.Name);
    }

    // Additional tests for error cases, validation, etc., can be added here
}
