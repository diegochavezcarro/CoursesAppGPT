using MediatR;
using CoursesApi.Models;
using CoursesApi.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesApi.Queries
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Course>
    {
        private readonly ICourseRepository _repository;

        public GetCourseByIdQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the course by ID from the repository
            var course = _repository.GetById(request.Id);

            // If course is not found, return null
            return Task.FromResult(course);
        }
    }
}
