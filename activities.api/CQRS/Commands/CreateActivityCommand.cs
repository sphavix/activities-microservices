using System.Runtime.CompilerServices;
using activities.api.Models;
using MediatR;

namespace activities.api.CQRS.Commands
{
    public class CreateActivityCommand : IRequest<Activity>
    {
        public string Title { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public CreateActivityCommand(string title, string description, bool isCompleted, DateTime createdAt)
        {
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;
        }
    }
}