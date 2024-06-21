using MediatR;

namespace activities.api.CQRS.Commands
{
    public class UpdateActivityCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UpdateActivityCommand(int id, string title, string description, bool isCompleted, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;
        }
    }
}