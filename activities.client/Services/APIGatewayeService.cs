using System.Net;
using System.Text;
using activities.client.Models;
using Newtonsoft.Json;

namespace activities.client.Services
{
    public class APIGatewayService
    {
        private string _baseUrl = "http://localhost:5131/api/activities";
        private HttpClient _client = new HttpClient();

        public List<Activity> GetActivities()
        {
            List<Activity> activities = new List<Activity>();

            //This code below is implemented when the API is using HTTPS
            // if(_baseUrl.Trim().Substring(0, 4).ToLower() == "http")
            // {
            //     ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // }

            try
            {
                HttpResponseMessage response = _client.GetAsync(_baseUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Activity>>(result);

                    if(data != null)
                    {
                        activities = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to retrieve activities. Error: {result}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving activities: {ex.Message}");
            }
            finally{ }
            return activities;
        }

        public Activity CreateActivity(Activity activity)
        {
            string payload = JsonConvert.SerializeObject(activity);
            try
            {
                HttpResponseMessage response = _client.PostAsync(_baseUrl, 
                                                new StringContent(payload, Encoding.UTF8, "application/json")).Result;
                if(response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Activity>(content);
                    if(data != null)
                    {
                        activity = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to create activity. Error: {result}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating activity: {ex.Message}");
            }
            finally{ }
            return activity;
        }

        public Activity GetActivity(int id)
        {
            Activity activity = new Activity();
            _baseUrl = _baseUrl + "/" + id;

            try
            {
                HttpResponseMessage response = _client.GetAsync(_baseUrl).Result;

                if(response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Activity>(content);
                    if(data != null)
                    {
                        activity = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to retrieve activity. Error: {result}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving activity: {ex.Message}");
            }
            finally{ }
            return activity;
        }

        public void UpdateActivity(Activity activity)
        {
            int id = activity.Id;
            _baseUrl = _baseUrl + "/" + id;

            string payload = JsonConvert.SerializeObject(activity);

            try
            {
                HttpResponseMessage response = _client.PutAsync(_baseUrl, 
                                    new StringContent(payload, Encoding.UTF8, "application/json")).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                if(!response.IsSuccessStatusCode)
                {
                    
                    // var data = JsonConvert.DeserializeObject<Activity>(content);
                    // if(data != null)
                    // {
                    //     activity = data;
                    // }
                    throw new Exception($"Failed to update activity. Error: {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating activity: {ex.Message}");
            }
            finally{ }
            return;
        }

        public void DeleteActivity(int id)
        {
            _baseUrl = _baseUrl + "/" + id;

            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_baseUrl).Result;
                if(!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to delete activity. Error: {result}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting activity: {ex.Message}");
            }
            finally{ }
            return;
        
        }
    }
}