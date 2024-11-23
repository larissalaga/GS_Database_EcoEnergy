using EcoEnergy.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy.Controllers
{
    public class MenuInicialController : Controller
    {
        private readonly IUsuarioRepository _usuario;
        public MenuInicialController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
