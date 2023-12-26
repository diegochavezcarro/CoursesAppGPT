using MediatR;
using CoursesApi.Models;
using CoursesApi.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesApi.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Course>
    {
        private readonly ICourseRepository _repository;

        public CreateCourseCommandHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public Task<Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            // Add the new course to the repository
            var createdCourse = _repository.Add(request.Course);

            // Return the newly created course
            return Task.FromResult(createdCourse);
        }
    }
}
