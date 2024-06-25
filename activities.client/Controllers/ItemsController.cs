using Microsoft.AspNetCore.Mvc;

namespace activities.client.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Id"] = id;
            return View();
        }
    }
}