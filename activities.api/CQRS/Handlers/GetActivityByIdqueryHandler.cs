using activities.api.CQRS.Queries;
using activities.api.Models;
using activities.api.Repositories;
using MediatR;

namespace activities.api.CQRS.Handlers
{
    public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, Activity>
    {
        private readonly IActivityRepository _repository;

        public GetActivityByIdQueryHandler(IActivityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Activity> Handle(GetActivityByIdQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetActivityAsync(query.Id);
        }
    }
}