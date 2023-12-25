using Microsoft.AspNetCore.Mvc;
using CoursesApi.Services;
using CoursesApi.Models;

namespace CoursesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        // CourseService is injected by the DI framework
        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        /// <summary>
        /// Represents an action result that performs an HTTP GET operation and returns an HTTP status code and a collection of courses.
        /// </summary>
        /// <returns>An IActionResult object that represents the HTTP response containing the collection of courses.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetAll()
        {
            var courses = _service.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var course = _service.GetCourseById(id);
            if (course == null) return NotFound();
            return Ok(course);
        }
    }
}
