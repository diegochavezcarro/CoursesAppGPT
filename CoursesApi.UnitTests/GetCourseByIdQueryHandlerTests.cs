using Xunit;
using Moq;
using CoursesApi.Queries;
using CoursesApi.Models;
using CoursesApi.Repository;
using System.Threading.Tasks;

public class GetCourseByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsCorrectCourse()
    {
        // Arrange
        var mockRepo = new Mock<ICourseRepository>();
        var testCourse = new Course { Id = 1, Name = "Test Course" };
        mockRepo.Setup(repo => repo.GetById(1)).Returns(testCourse);
        var handler = new GetCourseByIdQueryHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetCourseByIdQuery { Id = 1 }, default);

        // Assert
        Assert.Equal("Test Course", result.Name);
    }
}
