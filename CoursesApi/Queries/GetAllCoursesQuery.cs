using MediatR;
using CoursesApi.Models;
using System.Collections.Generic;

namespace CoursesApi.Queries
{
    public class GetAllCoursesQuery : IRequest<IEnumerable<Course>>
    {
    }
}
