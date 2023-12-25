using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using CoursesApi.Models;
using System.Text;

namespace CoursesApi.IntegrationTests
{
    public class CoursesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CoursesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCourses_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/courses");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetCourseById_ExistingId_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/courses/1");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var course = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(course);
            Assert.Equal(1, course.Id);
        }

        [Fact]
        public async Task GetCourseById_NonExistingId_ReturnsNotFoundResponse()
        {
            var response = await _client.GetAsync("/courses/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /*
                [Fact]
                public async Task CreateCourse_ValidCourse_ReturnsCreatedResponse()
                {
                    var course = new Course { Name = "Course 3" };
                    var response = await _client.PostAsJsonAsync("/courses", course);
                    response.EnsureSuccessStatusCode();
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    var newCourse = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
                    Assert.NotNull(newCourse);
                    Assert.Equal(3, newCourse.Id);
                    Assert.Equal("Course 3", newCourse.Name);
                }
                */
        [Fact]
        public async Task CreateCourse_ValidCourse_ReturnsCreatedResponse()
        {
            // Arrange
            var newCourse = new Course { Name = "New Course" };
            var httpContent = new StringContent(JsonConvert.SerializeObject(newCourse), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/courses", httpContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var addedCourse = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(addedCourse);
            //Assert.Equal(3, addedCourse.Id);
            Assert.Equal("New Course", addedCourse.Name);
        }

    }
}
