using System.ComponentModel.DataAnnotations;

namespace activities.client.Models
{
    public class Activity
    {
         public int Id { get; set; }
        public string Title { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}