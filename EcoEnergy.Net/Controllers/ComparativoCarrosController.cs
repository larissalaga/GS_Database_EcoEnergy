using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy.Controllers
{
    public class ComparativoCarrosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
