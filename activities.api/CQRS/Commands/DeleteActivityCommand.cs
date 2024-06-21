using MediatR;

namespace activities.api.CQRS.Commands
{
    public class DeleteActivityCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}