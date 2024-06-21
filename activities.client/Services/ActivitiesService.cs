using System.Net.Http;
using System.Threading.Tasks;
using activities.client.Models;

namespace activities.client.Services
{
    public class ActivitiesServices
    {
        private readonly HttpClient _httpClient;

        public ActivitiesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            var response = await _httpClient.GetStringAsAsync("http://localhost:5131/api/activities");
            return JsonConvert.DeserializeObject<IEnumerable<Activity>>(response);
        }
    }
}