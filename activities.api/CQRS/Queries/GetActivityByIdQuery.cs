using activities.api.Models;
using MediatR;

namespace activities.api.CQRS.Queries
{
    public class GetActivityByIdQuery : IRequest<Activity>
    {
        public int Id { get; set; }
    }
}