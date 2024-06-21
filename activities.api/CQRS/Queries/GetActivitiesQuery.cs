using activities.api.Models;
using MediatR;

namespace activities.api.CQRS.Queries
{
    public class GetActivitiesQuery : IRequest<IEnumerable<Activity>> {}
}