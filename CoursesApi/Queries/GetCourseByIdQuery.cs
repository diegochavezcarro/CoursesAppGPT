using MediatR;
using CoursesApi.Models;

namespace CoursesApi.Queries
{
    public class GetCourseByIdQuery : IRequest<Course>
    {
        public int Id { get; set; }
    }
}
