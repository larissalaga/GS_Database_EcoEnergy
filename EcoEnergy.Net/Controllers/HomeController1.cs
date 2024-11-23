using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
