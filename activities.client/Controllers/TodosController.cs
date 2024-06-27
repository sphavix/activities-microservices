using System.Linq.Expressions;
using activities.client.Models;
using activities.client.Services;
using Microsoft.AspNetCore.Mvc;

namespace activities.client.Controllers
{
    public class TodosController : Controller
    {
        private readonly APIGatewayService _apiGatewayService;

        public TodosController(APIGatewayService apiGatewayService)
        {
            _apiGatewayService = apiGatewayService;
        }
        public IActionResult Index()
        {
            List<Activity> activities;
            activities = _apiGatewayService.GetActivities();
            return View(activities);
        }

        public IActionResult Details(int id)
        {
            //Activity activity = new Activity();
            var activity = _apiGatewayService.GetActivity(id);
            if(activity is null)
            {
                return NotFound();
            }

            return View(activity);
        }

        public IActionResult Create()
        {
            Activity activity = new Activity();
            return View(activity);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Activity activity)
        {
            _apiGatewayService.CreateActivity(activity);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var activity = _apiGatewayService.GetActivity(id);
            return View(activity);
        }

        [HttpPost]
        public IActionResult Edit(Activity activity)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _apiGatewayService.UpdateActivity(activity);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View(ex.Message);
                }
            }
            return View();
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var activity = _apiGatewayService.GetActivity(id);
            return View(activity);
        }

        [HttpPost]
        public IActionResult Delete(Activity activity)
        {
            _apiGatewayService.DeleteActivity(activity.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}