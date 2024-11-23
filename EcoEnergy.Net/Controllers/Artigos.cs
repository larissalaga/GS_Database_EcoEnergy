using Microsoft.AspNetCore.Mvc;

namespace WebApplicationOdontoPrev.Controllers
{
    public class ArtigosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
