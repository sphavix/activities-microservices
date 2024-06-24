using activities.client.Models;
using activities.client.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace activities.client.Controllers
{
    public class ActivitiesController : Controller
    {
        Uri baseUrl = new Uri("http://localhost:5131/api");
        private readonly ILogger<ActivitiesController> _logger;
        private readonly ActivitiesService _service;
        private readonly HttpClient _httpClient;

        public ActivitiesController(ActivitiesService service, ILogger<ActivitiesController> logger)
        {
            _logger = logger;
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }
        public async Task<IActionResult> Index()
        {
            var activities = await _service.GetActivitiesAsync();
            return View(activities);
        }

        public async Task<IActionResult> Details(int id)
        {
            var activity = await _service.GetActivityAsync(id);
            if (activity is null)
            {
                return BadRequest();
            }
            return View(activity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateActivity(activity);
                return RedirectToAction(nameof(Index));
            }
            return View(activity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // var activity = await _service.GetActivityAsync(id);
            // if(activity is null)
            // {
            //     return NotFound();
            // }
            try
            {
                Activity activity = new Activity();
                HttpResponseMessage response = _httpClient.GetAsync(baseUrl + "/activities/" + id).Result;

                if(response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    activity = JsonConvert.DeserializeObject<Activity>(data)!;
                }
                return View(activity);
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Activity activity)
        {
           if(ModelState.IsValid)
           {
                try
                {
                    var result = await _service.UpdateActivity(id, activity);
                    TempData["successMessage"] = "Activity updated successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    _logger.LogError(ex.Message);
                    return View();
                }
           }
           return View(activity);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _service.GetActivityAsync(id);
            if (activity is null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteActivity(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
