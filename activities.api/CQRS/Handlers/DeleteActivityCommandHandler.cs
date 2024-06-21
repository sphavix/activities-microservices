using activities.api.CQRS.Commands;
using activities.api.Repositories;
using MediatR;

namespace activities.api.CQRS.Handlers
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, int>
    {
        private readonly IActivityRepository _repository;

        public DeleteActivityCommandHandler(IActivityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(DeleteActivityCommand command, CancellationToken cancellationToken)
        {
            var activity = await _repository.DeleteActivityAsync(command.Id);
            if(activity < 0)
            {
                return default;
            }

            await _repository.DeleteActivityAsync(command.Id);
            return command.Id;
        }
    }
}