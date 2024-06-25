using activities.api.CQRS.Commands;
using activities.api.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace activities.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _mediator.Send(new GetActivitiesQuery());
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await _mediator.Send(new GetActivityByIdQuery { Id = id });
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var result = await _mediator.Send(new DeleteActivityCommand { Id = id });
            return Ok(result);
        }
    }
}