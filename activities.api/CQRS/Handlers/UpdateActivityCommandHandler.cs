using activities.api.CQRS.Commands;
using activities.api.Repositories;
using MediatR;

namespace activities.api.CQRS.Handlers
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, int>
    {
        private readonly IActivityRepository _repository;
        public UpdateActivityCommandHandler(IActivityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(UpdateActivityCommand command, CancellationToken cancellationToken)
        {
            var activity = await _repository.GetActivityAsync(command.Id);
            if (activity == null)
            {
                return default;
            }

            // We can also use AutoMapper here to map the properties from the command to the activity object
            activity.Title = command.Title;
            activity.Description = command.Description;
            activity.IsCompleted = command.IsCompleted;
            activity.CreatedAt = DateTime.UtcNow;

            await _repository.UpdateActivityAsync(activity.Id, activity);
            return activity.Id;
        }
    }
}