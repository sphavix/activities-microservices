using activities.client.Models;
using activities.client.Services;
using Microsoft.AspNetCore.Mvc;

namespace activities.client.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ActivitiesService _service;

        public ActivitiesController(ActivitiesService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
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

        public async Task<IActionResult> Edit(int id)
        {
            var activity = await _service.GetActivityAsync(id);
            if(activity is null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Activity activity)
        {
            if(id != activity.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _service.UpdateActivity(id, activity);
                return RedirectToAction(nameof(Index));
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
