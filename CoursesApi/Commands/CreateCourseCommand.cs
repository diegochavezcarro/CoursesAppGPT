using MediatR;
using CoursesApi.Models;

namespace CoursesApi.Commands
{
    public class CreateCourseCommand : IRequest<Course>
    {
        public Course Course { get; set; }
    }
}
