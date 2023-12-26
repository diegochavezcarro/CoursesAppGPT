using MediatR;
using CoursesApi.Models;
using CoursesApi.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesApi.Queries
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<Course>>
    {
        private readonly ICourseRepository _repository;

        public GetAllCoursesQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Course>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}
