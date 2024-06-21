using Newtonsoft.Json;
using activities.api.Data;
using activities.api.Models;
using Microsoft.EntityFrameworkCore;
namespace activities.api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ActivitiesDbContext _context;
        public ActivityRepository(ActivitiesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<Activity> GetActivityAsync(int id)
        {
            return await _context.Activities.Where(a => a.Id == id).FirstOrDefaultAsync()
                                            .ConfigureAwait(true);
        }

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<int> UpdateActivityAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> DeleteActivityAsync(int id)
        {
            var activity = await GetActivityAsync(id);
            _context.Activities.Remove(activity);
            return await _context.SaveChangesAsync();
        }
    }
}