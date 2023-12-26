using Xunit;
using Moq;
using CoursesApi.Queries;
using CoursesApi.Models;
using CoursesApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class GetAllCoursesQueryTests
{
    [Fact]
    public async Task Handle_ReturnsAllCourses()
    {
        // Arrange
        var mockRepo = new Mock<ICourseRepository>();
        var testCourses = new List<Course>
        {
            new Course { Id = 1, Name = "Course 1" },
            new Course { Id = 2, Name = "Course 2" }
        };

        mockRepo.Setup(repo => repo.GetAll()).Returns(testCourses);
        var handler = new GetAllCoursesQueryHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllCoursesQuery(), new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Course 1", result.First().Name);
        Assert.Equal("Course 2", result.Last().Name);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyListIfNoCourses()
    {
        // Arrange
        var mockRepo = new Mock<ICourseRepository>();
        mockRepo.Setup(repo => repo.GetAll()).Returns(new List<Course>());
        var handler = new GetAllCoursesQueryHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllCoursesQuery(), new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
