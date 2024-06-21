using activities.api.CQRS.Queries;
using activities.api.Models;
using activities.api.Repositories;
using MediatR;

namespace activities.api.CQRS.Handlers
{
    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, IEnumerable<Activity>>
    {
        private readonly IActivityRepository _repository;
        public GetActivitiesQueryHandler(IActivityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Activity>> Handle(GetActivitiesQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetActivitiesAsync();
        }
    }
}