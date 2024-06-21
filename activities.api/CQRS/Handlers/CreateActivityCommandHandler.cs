using activities.api.CQRS.Commands;
using activities.api.Models;
using activities.api.Repositories;
using MediatR;

namespace activities.api.CQRS.Handlers
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Activity>
    {
        private readonly IActivityRepository _repository;

        public CreateActivityCommandHandler(IActivityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Activity> Handle(CreateActivityCommand command, CancellationToken cancellationToken)
        {
           var activity = new Activity
            {
                Title = command.Title,
                Description = command.Description,
                IsCompleted = command.IsCompleted,
                CreatedAt = DateTime.UtcNow,
            };

            await _repository.CreateActivityAsync(activity);
            return activity;
        }
    }
}