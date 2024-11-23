using EcoEnergy.Data;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoEnergy.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuario;
        private readonly DataContext _context;

        public LoginController(IUsuarioRepository usuario, DataContext context)
        {
            _usuario = usuario;
            _context = context;
        }
        private async Task<bool> BuscarUsuario(string email)
        {
            var usuario = await _usuario.GetByEmail(email);
            if (usuario == null)
            {
                return false;
            }
            HttpContext.Session.SetInt32("UserId", usuario.IdUsuario);
            HttpContext.Session.SetString("UserName", usuario.NmUsuario);
            return true;
        }
        public async Task<IActionResult> Login(string email)
        {
            try
            {
                var usuarioEncontrado = await BuscarUsuario(email);
                if (usuarioEncontrado)
                    return RedirectToAction("Index", "MenuInicial");
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
