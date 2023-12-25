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

        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _service.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _service.GetCourseById(id);
            if (course == null) return NotFound();
            return Ok(course);
        }
    }
}
