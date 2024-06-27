using activities.api.Models;

namespace activities.api.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetActivityAsync(int id);
        Task<IEnumerable<Activity>> GetActivitiesAsync();
        Task<Activity> CreateActivityAsync(Activity activity);
        Task<int> UpdateActivityAsync(int id, Activity activity);
        Task<int> DeleteActivityAsync(int id);
    }
}