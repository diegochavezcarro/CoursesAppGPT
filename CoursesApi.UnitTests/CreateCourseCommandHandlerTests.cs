using Xunit;
using Moq;
using CoursesApi.Commands;
using CoursesApi.Models;
using CoursesApi.Repository;
using System.Threading.Tasks;

public class CreateCourseCommandHandlerTests
{
    [Fact]
    public async Task Handle_CreatesCourseSuccessfully()
    {
        // Arrange
        var mockRepo = new Mock<ICourseRepository>();
        var newCourse = new Course { Name = "New Course" };
        mockRepo.Setup(repo => repo.Add(It.IsAny<Course>())).Returns(newCourse);
        var handler = new CreateCourseCommandHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new CreateCourseCommand { Course = newCourse }, default);

        // Assert
        Assert.Equal("New Course", result.Name);
    }
}
