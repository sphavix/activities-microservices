using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using activities.client.Models;
using Newtonsoft.Json;

namespace activities.client.Services
{
    public class ActivitiesService
    {
        private readonly HttpClient _httpClient;

        public ActivitiesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            var response = await _httpClient.GetStringAsync($"http://localhost:5131/api/activities");
            return JsonConvert.DeserializeObject<IEnumerable<Activity>>(response)!;
        }

        public async Task<Activity> GetActivityAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"http://localhost:5131/api/activities/{id}");
            return JsonConvert.DeserializeObject<Activity>(response)!;
        }

        public async Task<Activity> CreateActivity(Activity activity)
        {
            var jsonData = JsonConvert.SerializeObject(activity);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"http://localhost:5131/api/activities", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Activity>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task UpdateActivity(int id, Activity activity)
        {
            var jsonData = JsonConvert.SerializeObject(activity);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5131/api/activities/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteActivity(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/activities/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}