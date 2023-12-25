using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using CoursesApi.Models;

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

        
    }
}
