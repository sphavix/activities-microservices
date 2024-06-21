using Newtonsoft.Json;

namespace activities.api.Repositories
{
    public class ActivityRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "activities.json");

        public List<Activity> GetActivities()
        {
            if(!File.Exists(_filePath))
            {
                return new List<Activity>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Activity>>(json)!;
        }

        public void SaveActivities(List<Activity> activities)
        {
            var json = JsonConvert.SerializeObject(activities, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void AddActivity(Activity activity)
        {
            var activities = GetActivities();
            activity.Id = activities.Any() ? activities.Max(a => a.Id) + 1 : 1;
            activities.Add(activity);
            SaveActivities(activities);
        }

        public void UpdateActivity(Activity activity)
        {
            var activities = GetActivities();
            var existingActivity = activities.FindIndex(a => a.Id == activity.Id);
            if(existingActivity != -1)
            {
                activities[existingActivity] = activity;
                SaveActivities(activities);
            }
        }

        public void DeleteActivity(int id)
        {
            var activities = GetActivities();
            var existingActivity = activities.FirstOrDefault(a => a.Id == id);
            if(existingActivity != null)
            {
                activities.Remove(existingActivity);
                SaveActivities(activities);
            }
        }
    }
}